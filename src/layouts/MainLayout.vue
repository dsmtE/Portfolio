<template>
<q-layout view="hHh lpR fFf">
    <CircleCursor/>
    <LanguageSwitcher id="languageSwitcher" class="q-ma-sm"/>
    <q-page-container id="pageContainer">
      <NavBar :items="items" />
      <transition name="fade" mode="out-in">
        <router-view/>
      </transition>
    </q-page-container>
  </q-layout>
</template>

<script>

export default {
  name: 'MainLayout',
  components: {
    NavBar: () => import('components/NavBar.vue'),
    CircleCursor: () => import('components/CircleCursor.vue'),
    LanguageSwitcher: () => import('components/LanguageSwitcher.vue'),
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

  #languageSwitcher {
    position: fixed;
    z-index: 10;
    left: 0;
    top: 0;
  }

  .fade-enter-active, .fade-leave-active {
    transition: opacity .5s;
  }

  .fade-enter, .fade-leave-to {
    opacity: 0;
  }

</style>
