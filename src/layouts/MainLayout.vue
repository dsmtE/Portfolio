<template>
<q-layout view="hHh lpR fFf">
    <CircleCursor/>
    <q-page-container id="pageContainer">
      <NavBar :items="items" />
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>

export default {
  name: 'MainLayout',
  components: {
    NavBar: () => import('components/NavBar.vue'),
    CircleCursor: () => import('components/CircleCursor.vue'),
  },
  data() {
    return {
      items: [
        {
          label: 'home',
          to: '/',
        },
        {
          label: 'Projects',
          to: '/projects',
        },
      ],
    };
  },
};
</script>

<style lang="scss">

  #pageContainer {
    cursor: none;
  }

</style>
