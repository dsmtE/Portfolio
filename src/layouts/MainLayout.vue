<template>
<q-layout view="hHh lpR fFf">
    <CircleCursor/>
    <q-page-container id="pageContainer">
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>

export default {
  name: 'MainLayout',
  components: {
    CircleCursor: () => import('components/CircleCursor.vue'),
  },
  data() {
    return {
    };
  },
};
</script>

<style lang="scss">

  #pageContainer {
    cursor: none;
  }

</style>
