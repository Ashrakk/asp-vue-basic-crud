<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide" @before-show="onBeforeShow">
    <q-card class="q-dialog-plugin min-w-450">
      <q-card-section class="q-gutter-md">
        <div v-if="mode === 'create'"
             class="text-h6">Create new</div>
        <div v-else
             class="text-h6">Edit walk - {{toEdit.id}}</div>
        <q-form
          ref="form">
          <q-input
            type="text"
            filled
            label="Description"
            v-model="description"
            :rules="[val => val && val.length > 4 || 'Required']"
          />
          <q-input
            type="number"
            style="appearance: none"
            filled
            label="Length in Km"
            v-model.number="lengthInKm"
            :rules="[val => val && val > 0 || 'Required']"
          />
          <q-select
            filled
            label="Difficulty"
            v-model="diffFK"
            :options="diffOptions"
          />
          <q-select
            class="q-my-lg"
            filled
            label="Region"
            v-model="regionFK"
            :options="regionOptions"
          />
          <q-btn
            class="float-right q-mb-md"
            icon="add"
            type="submit"
            color="primary"
            @click="onAction"
          ><span v-if="mode === 'create'">Create</span>
            <span v-else>Edit</span>
          </q-btn>
        </q-form>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
  import {ref, onMounted} from "vue"
  import {useQuasar} from "quasar";
  import {useDialogPluginComponent} from 'quasar'
  import {QForm} from "quasar";
  import {IWalks} from "boot/interfaces/IWalks";
  import {useWalkApiStore} from "stores/walk-api-store";
  import {useRegionApiStore} from "stores/region-api-store";
  import {useDifficultyApiStore} from "stores/difficulty-api-store";

  const $q = useQuasar();
  const walkApi = useWalkApiStore();
  const regionApi = useRegionApiStore();
  const difficultyApi = useDifficultyApiStore();

  const form = ref(QForm);
  const description = ref("");
  const lengthInKm = ref(0);
  const regionFK = ref();
  const diffFK = ref();
  const image = ref("");
  const diffOptions = ref([]);
  const regionOptions = ref([]);

  const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } = useDialogPluginComponent()
  const emit = defineEmits([...useDialogPluginComponent.emits]);
  const props = defineProps({
    mode: {
      type: String,
      validator: (value: string) => ['create', 'edit'].includes(value),
      required: true
    },
    toEdit: {
      type: Object as () => IWalks
    }
  });

  async function onAction() {
    const valid = await form.value.validate()
    if(!valid)
      return;

    let data: IWalks = {
      description: description.value,
      image: image.value,
      lengthKm: lengthInKm.value,
      regionFK: regionFK.value.value,
      difficultyFK: diffFK.value.value
    };

    var result;

    if(props.mode == 'edit' && props.toEdit)
    {
      data.id = props.toEdit.id;
      result = await onEdit(data);
    }
    else
      result = await onCreate(data);

    if(result != false)
      onDialogOK();
    else
      onDialogCancel();
  }

  async function onCreate(model: IWalks) {
    const result = await walkApi.postCreate(model);
    if(!result)
    {
      $q.notify({
        type: 'negative',
        message: "A problem has occurred during the creation process!"
      })
    }
    else
    {
      $q.notify({
        type: 'positive',
        message: "Successfully created a new Walk!"
      })
    }
    return result;
  }

  async function onEdit(model: IWalks) {
    const result = await walkApi.postUpdate(model);
    if(!result)
    {
      $q.notify({
        type: 'negative',
        message: "A problem has occurred during the editing process!"
      })
    }
    else
    {
      $q.notify({
        type: 'positive',
        message: "Successfully Edited Walk " + model.id + "!"
      })
    }
    return result;
  }

  function cleanData() {
    description.value = "";
    lengthInKm.value = 0;
    regionFK.value = null;
    diffFK.value = null;
    image.value = "";
  }

  function loadData() {
    if(props.toEdit) {
      description.value = props.toEdit.description;
      lengthInKm.value = props.toEdit.lengthKm;
      regionFK.value = regionOptions.value.find((ar) => ar.value == props.toEdit.region.id);
      diffFK.value = diffOptions.value.find((ar) => ar.value == props.toEdit.difficulty.id);
      image.value = props.toEdit.image;
    }
  }

  async function loadDifficulties() {
    const result = await difficultyApi.getAll();
    if(!result)
    {
      $q.notify({
        type: 'negative',
        message: "A problem has occurred while loading data from the database!"
      })
      onDialogCancel();
      return;
    }
    diffOptions.value = result.map((item: any) => ({
      label: item.name,
      value: item.id
    }));
  }

  async function loadRegions() {
    const result = await regionApi.getAll();
    if(!result)
    {
      $q.notify({
        type: 'negative',
        message: "A problem has occurred while loading data from the database!"
      })
      onDialogCancel();
      return;
    }

    regionOptions.value = result.map((item: any) => ({
      label: item.name,
      value: item.id
    }));
  }

  function onBeforeShow() {
    cleanData();
    if(props.mode == 'edit')
      loadData();
    else
    {
      diffFK.value = diffOptions.value[0];
      regionFK.value = regionOptions.value[0];
    }
  }

  onMounted(async () => {
    await loadDifficulties();
    await loadRegions();
    onBeforeShow();
  });

</script>
