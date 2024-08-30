<template>
  <q-page class="mt-12 text-center">
    <h1>Hey there!</h1>
    <div class="max-w-800 q-mx-auto">
      <WalksPaginated/>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import WalksPaginated from "components/WalksPaginated.vue";

defineOptions({
  name: 'IndexPage'
});

</script>
