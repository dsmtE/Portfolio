<template>
  <div class="tile col-12 col-md-2" :class="{'hover': hover}"
    @mouseover="hover = true" @mouseout="hover = false">
    <div class="background">
      <q-img @load="computeAverage" ref="img" :src="infos.img" basic :ratio="4/3" />
    </div>
    <div class="overlay" :style="{'background-color': averageColor || overlayColor}"></div>
    <div class="details q-pa-md">
      <span class="name"> {{ infos.name }} </span>
      <br/>
      <span class="description"> {{ infos.description }} </span>

    </div>
  </div>
</template>

<script>
import { getAverage } from '@js/colorExtraction.js';

export default {
  name: 'ProjectTile',
  data() {
    return {
      hover: false,
      averageColor: undefined,
    };
  },
  props: {
    infos: {
      type: Object,
      require: true,
      validator: obj => obj.name && obj.img,
    },
    overlayColor: {
      type: String,
      require: false,
    },
  },
  computed: {
  },
  mounted() {
  },
  methods: {
    computeAverage() {
      if (!this.overlayColor) { // compute averageColor only if overlayColor isn't provided
        const average = getAverage(this.$refs.img.image, 2000).map(x => Math.floor(x));
        this.averageColor = `rgba(${average[0]}, ${average[1]}, ${average[2]}, 0.7)`;
      }
    },
  },
};
</script>

<style lang="scss" scoped>

.tile {
  overflow: hidden;
  position: relative;

  .overlay {
    position: absolute;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    opacity: 0;
    transition: opacity 0.5s ease-out 0.3s;
  }

  .details {
    position: absolute;
    left: 0;
    top: 0;
    width: 80%;
    height: 100%;
    color: white;
    transition: background-color 0s linear 0.8s, opacity 0.5s ease-out 0.3s;

    .name {
      position: relative;
      font-family: 'Oswald', sans-serif;
      font-size: 2em;
      top: 5em;
      transition: top 0s linear 0.8s, opacity 0.5s ease-out 0.3s;
      opacity: 0;
    }
    .description {
      position: relative;
      font-size: 1.5em;
      transition: opacity 0.5s ease-in;
      opacity: 0;
    }
  }
}

.hover.tile {

  .overlay {
    opacity: 1;
    transition: opacity 0.4s ease-in;
  }

  .details {
    .name {
      top: 0;
      transition: top 0.3s ease-out, opacity 0.2s linear 0.1s;
      opacity: 1;
    }
    .description {
      transition: opacity 0.4s ease-in 0.3s;
      opacity: 1;
    }
  }
}

</style>
