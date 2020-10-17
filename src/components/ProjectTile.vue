<template>
  <q-card square flat class="col-12 col-md-2">
    <q-img :src="data.img" basic :ratio="1">
      <div class="absolute-bottom text-subtitle2 text-center">
       {{ data.name }}
      </div>
    </q-img>
  </q-card>
</template>

<script>

export default {
  name: 'ProjectTile',
  props: {
    data: {
      type: Object,
      require: true,
      validator: array => array.name && array.img,
    },
  },
};
</script>

<style>

</style>
