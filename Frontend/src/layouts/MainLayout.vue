<template>
  <q-layout view="hHh lpR fFf">

    <q-header reveal bordered class="bg-primary text-white" height-hint="98">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar>
            <img src="https://cdn.quasar.dev/logo-v2/svg/logo-mono-white.svg">
          </q-avatar>
          Corso ASP
        </q-toolbar-title>
        <q-tabs align="left">
          <q-route-tab to="/" label="Homepage" />
          <q-route-tab to="/" label="Another Page" />
          <q-route-tab to="/" label="And Another Another Page" />
        </q-tabs>
      </q-toolbar>


    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>
    <!--
    <q-footer elevated class="bg-grey-8 text-white">
      <q-toolbar>
        <q-toolbar-title>
          <div>Corso ASP</div>
        </q-toolbar-title>
      </q-toolbar>
    </q-footer>
    -->
  </q-layout>
</template>

<script setup lang="ts">

defineOptions({
  name: 'MainLayout'
});

</script>
