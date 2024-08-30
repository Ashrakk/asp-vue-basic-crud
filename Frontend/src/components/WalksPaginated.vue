<template>
  <div class="full-width">
    <div class="row">
      <div class="col-auto q-pb-md ">
        <q-btn @click="refreshData" class="q-mr-sm" color="white" text-color="dark">Refresh</q-btn>
        <q-btn @click="onCreate" class="q-mr-sm" color="primary" text-color="white">Create New</q-btn>
      </div>
      <div class="col-auto q-ml-auto">
        <q-btn @click="filterStatus = !filterStatus" class="q-ml-auto" style="width: 110px" color="primary" text-color="white" icon="filter_list" align="between">Filter</q-btn>
      </div>
    </div>
    <transition name="fade">
      <div class="row" v-show="filterStatus">
        <div class="col-auto q-ml-auto">
          <q-select
            filled dense
            class="q-mb-md"
            style="width: 200px"
            label="Difficulty"
            v-model="diffFilter"
            :options="diffFilterOptions"
            @update:model-value="applyFilter"
          >
          </q-select>
        </div>
      </div>
    </transition>
    <q-table
      ref="tableRef"
      flat bordered
      style="height: 400px"
      class="full-width my-sticky-dynamic"
      separator="horizontal"
      table-style="min-width: 100%"
      table-class="divide-blue-2"
      header-class="text-left text-xs font-medium uppercase tracking-wider text-blue-6"
      body-class="bg-white divide-y divide-blue-3"
      :loading="loading"
      :rows="data"
      row-key="id"
      :visible-columns="visibleCol"
      :columns="col"
      virtual-scroll
      :virtual-scroll-item-size="48"
      :virtual-scroll-sticky-size-start="48"
      :rows-per-page-options="[0]"
      :pagination="pagination"
      @virtual-scroll="onScroll"
    >
      <template v-slot:body-cell-actions="props">
        <q-td :props="props">
          <q-btn @click="onEdit(props.row)" fab-mini dense square outline icon="mode_edit" color="primary" aria-label="Edit" class="q-mr-sm" />
          <q-btn @click="onDelete(props.row)" fab-mini dense square outline icon="delete" color="negative" aria-label="Delete" />
        </q-td>
      </template>
      <template #loading>
        <q-inner-loading
          showing
          color="primary"
        />
      </template>
    </q-table>
  </div>
</template>

<script setup lang="ts">
import {ref, onMounted, computed, watch} from "vue";
  import {useWalkApiStore} from "stores/walk-api-store";
  import WalksPopup from "components/WalksPopup.vue";
  import {useQuasar} from "quasar";
import {useDifficultyApiStore} from "stores/difficulty-api-store";

  const pageSize = 50;
  const nextPage = ref(0);
  const totalCount = ref(0);
  const lastPage = computed(() => {return Math.ceil(totalCount.value / pageSize)});

  const $q = useQuasar();
  const walkApi = useWalkApiStore();
  const diffApi = useDifficultyApiStore();

  const filterStatus = ref(false);
  const tableRef = ref();
  const data = ref([]);
  const loading = ref(false);
  const visibleCol = ref(['ID, DESC, LENGTH_KM, DIFFICULTY_NAME, REGIONS_NAME, actions'])
  const diffOrder = ref([]);
  const col = [
    { name: 'ID', label: 'ID', field: 'id', sortable: true, required: true },
    { name: 'DESC', label: 'Description', field: 'description', required: true },
    { name: 'LENGTH_KM', label: 'Length in KM', field: 'lengthKm', sortable: true, required: true },
    { name: 'DIFFICULTY_NAME', label: 'Difficulty', sortable: true, field: row => row.difficulty?.name || 'N/A', required: true,
      sort: (a, b) => diffOrder.value.indexOf(a) - diffOrder.value.indexOf(b)},
    { name: 'REGIONS_NAME', label: 'Region', field: row => row.region?.name || 'N/A', sortable: true, required: true },
    { name: 'DIFFICULTY_ID', label: 'Difficulty', field: row => row.difficulty?.id || 'N/A' },
    { name: 'REGIONS_ID', label: 'Region', field: row => row.region?.id || 'N/A' },
    { name: 'actions', label: 'Action', required: true }
  ]
  const pagination = ref({
    sortBy: 'id',
    descending: false,
    rowsPerPage: 0,
  })

  const diffFilterOptions = ref([]);
  const diffFilter = ref(null);

  //IMITATE LAG
  const sleep = (ms: number) => new Promise((r) => setTimeout(r, ms));

  //Query "Walks"
  async function loadData(page: number) {
    loading.value = true;
    let response = null;

    console.log(diffFilter.value, page, pageSize);

    if(filterStatus && diffFilter.value)
        response = await walkApi.getPage(page, pageSize, diffFilter.value.value)
    else
      response = await walkApi.getPage(page, pageSize);

    if(response != null) {
      await sleep(600); // IMITATE LAG
      data.value = [...data.value, ...response.data];
      totalCount.value = response.totalCount;
    }
    else
    {
      $q.notify({
        type: 'negative',
        message: "A problem has occurred while refreshing!"
      })
    }
    loading.value = false;
  }

  async function onScroll({to, ref}) {
    const lastIndex = data.value.length - 1 ;

    if(nextPage.value < lastPage.value
        && to >= lastIndex
        && data.value.length >= 50)
    {
      nextPage.value++;
      await loadData(nextPage.value);
      ref.refresh();
    }
  }

  async function onDelete(prop) {
    data.value.splice(prop.index, 1);
    loading.value = true;
    const response = await walkApi.postDelete(prop.id);
    if(!response)
      $q.notify({
        type: 'negative',
        message: "A problem has occurred during the deletion process!"
      })
    await refreshData();
    loading.value = false;
  }

  function onEdit(prop) {
    $q.dialog({
      component: WalksPopup,
      componentProps: {
        mode: 'edit',
        toEdit: prop
      }
    }).onOk(async () => {
      await refreshData();
    })
  }

  async function onCreate() {
    $q.dialog({
      component: WalksPopup,
      componentProps: {
        mode: 'create',
        toEdit: null
      }
    }).onOk(async () => {
      await refreshData();
    })
  }

  //Called to reset the page
  async function refreshData() {
    loading.value = true;
    data.value = [];
    nextPage.value = 1;
    totalCount.value = 0;
    await loadData(nextPage.value);
    loading.value = false;
  }

  async function applyFilter() {
    if(filterStatus.value && diffFilter.value)
      await refreshData();
  }

  //Query "Difficulty"
  async function loadFilters() {
    const result = await diffApi.getAll();
    if(!result)
    {
      $q.notify({
        type: 'negative',
        message: "A problem has occurred while loading data from the database!"
      })
      return;
    }

    diffOrder.value = result.map(item => item.name);
    diffFilterOptions.value = result.map((item: any) => ({
      label: item.name,
      value: item.id
    }));
  }

  onMounted(async () => {
    await loadFilters();
    await refreshData();
  });

  watch(filterStatus, () => {
    if(filterStatus.value == false)
      diffFilter.value = null; // clean filter
  })

</script>
