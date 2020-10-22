<template>
  <q-page>
    <CurtainsWrapper v-if="this.curtainCanvas" :planes="[this.curtainCanvas]"/>
    <div id="curtainPlane" ref="curtainPlane">
      <canvas id="curtainCanvas" ref="curtainCanvas" data-sampler="uCanvas"></canvas>
    </div>

    <div class="fullHeight flex flex-center">
      <SocialsLinks id="socials" class="q-mr-sm" :items="socials"/>
      <div class="column content-center">
        <div class="col">
          <h2 class="q-mt-none q-mb-sm text-center">Enguerrand DE SMET</h2>
        </div>
        <div class="col">
          <h3 class="q-ma-none text-center"> {{ $t('title') }} </h3>
        </div>

        <q-card id="description" class="q-mt-xl q-pa-md">
          <q-card-section>
            <p class="q-ma-none text-h5 test-weight-medium"> {{ $t('landingDescription') }} </p>
          </q-card-section>
        </q-card>

      </div>
    </div>
  </q-page>
</template>

<script>

import fs from 'assets/shaders/landing.fs';
import vs from 'assets/shaders/landing.vs';

export default {
  name: 'Landing',
  components: {
    SocialsLinks: () => import('components/SocialsLinks.vue'),
    CurtainsWrapper: () => import('components/CurtainsWrapper.vue'),
  },
  data() {
    return {
      curtainCanvas: undefined,
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
  mounted() {
    this.curtainCanvas = {
      DOMElement: this.$refs.curtainPlane,
      fs,
      vs,
      uniforms: ['time', 'mousePosition'],
    };
  },
};
</script>

<style lang="scss">

#socials {
  position: absolute;
  right: 0;
}

#description {
  max-width: 50em;
}

#curtainPlane {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
}

#curtainPlane canvas {
  display: none;
}

</style>
