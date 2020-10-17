<template>
  <q-page >
    <div class="fullHeight flex flex-center">
      <div class="column content-center">
        <div class="col">
          <h2 class="q-mt-none q-mb-sm text-center">Enguerrand DE SMET</h2>
        </div>
        <div class="col">
          <h3 class="q-ma-none text-center"> {{ $t('title') }} </h3>
        </div>

        <q-card id="description" class="q-mt-xl q-pa-md">
          <q-card-section >
            <p class="q-ma-none text-h5 test-weight-medium"> {{ $t('landingDescription') }} </p>
          </q-card-section>
        </q-card>
        <SocialsLinks id="socials" class="q-mr-sm" :items="socials"/>
      </div>
    </div>
  </q-page>
</template>

<script>
export default {
  name: 'Landing',
  components: {
    SocialsLinks: () => import('components/SocialsLinks.vue'),
  },
  data() {
    return {
      socials: [
        {
          icon: 'fab fa-linkedin',
          url: 'https://www.linkedin.com/in/enguerrand-de-smet-1879a119b/',
        },
        {
          icon: 'fab fa-github',
          url: 'https://github.com/dsmtE',
        },
        {
          icon: 'fab fa-instagram',
          url: 'https://www.instagram.com/enguerrand_desmet/',
        },
      ],
    };
  },
};
</script>

<style lang="scss">

#socials {
  position: absolute;
  z-index: 10;
  right: 0;
}

#description {
  max-width: 50em;
}

</style>
