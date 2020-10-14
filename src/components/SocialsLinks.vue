<template>
  <div class="column">
      <div v-for="(item, id) in items" :key="id" class="col q-mb-sm">
        <q-btn flat :icon="item.icon" type="a" target="_blank" :href="item.url" />
      </div>
    </div>
</template>

<script>
export default {
  name: 'SocialsLinks',
  components: {
  },
  props: {
    items: {
      type: Array,
      require: true,
      validator: array => array.every(item => item.icon && item.url),
    },
  },
};
</script>

<style>

</style>
