<template>
  <dir class="nav row justify-end q-gutter-lg q-mr-xl" :style="{height: `${navHeight}px`}"
   @mouseover="hover = true" @mouseleave="hover = false">
    <router-link tag="div" class="flex flex-center justify-center"
      v-slot="{ href, navigate, isExactActive  }"
      v-for="(item, id) in items" :key="id" :to="item.to">
      <div class="link" :style="isExactActive  ? linksActiveStyle : linksStyle"
      :href="href" @click="navigate">
        {{ item.label }}
      </div>
    </router-link>
  </dir>
</template>

<script>
export default {
  name: 'NavBar',
  props: {
    items: {
      type: Array,
      require: true,
      validator: array => array.every(item => item.label && item.to),
    },
  },
  data() {
    return {
      hover: false,
      linkWidth: 80,
      navHeight: 140,
      color: 'rgba(0, 0, 0, 0.5)',
      hoverColor: 'rgba(0, 0, 0, 1)',
    };
  },
  computed: {
    currentRoute() {
      return this.$route.currentRoute;
    },
    linksStyle() {
      return {
        width: `${this.linkWidth}px`,
        'background-color': this.hover ? this.hoverColor : this.color,
        transform: `translate(0, ${this.hover ? 0 : -this.navHeight * 0.6}px)`,
        transition: 'transform 0.5s ease, background-color 0.8s ease',
      };
    },
    linksActiveStyle() {
      const heightReduction = 0.5;
      return {
        width: `${this.linkWidth}px`,
        'background-color': this.hover ? this.hoverColor : this.color,
        transform: `translate(0, ${this.hover ? 0 : -this.navHeight * heightReduction * 0.2}px)`,
        height: `${100 * (1 - !this.hover * heightReduction)}%`,
        transition: 'transform 0.6s ease, background-color 0.8s ease, height 0.6s ease',
      };
    },
  },
};
</script>

<style lang="scss" scoped>

  .nav {
    position: fixed;
    top: 0;
    right: 0;
    z-index: 100;
  }

  .link {
    color: white;
    // cursor: pointer;
    transform-origin: top;
  }
</style>
