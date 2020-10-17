<template>
  <div :style="{width: width}">
      <h5 class="title q-ma-none q-pa-none">
        <q-icon name="search" size="md" /> {{ title }}
      </h5>
      <q-separator spaced="sm" size="2px"/>
      <div class="row q-gutter-md">
        <q-chip v-for="(state, item) in itemsStates" :key="item"
          class="col-auto link flex flex-center" square
          :text-color="state ? 'white' : 'black'"
          :color="state ? 'black' : 'white'"
          :icon="state ? 'done' : 'remove'"
          clickable @click="changeState(item)">
            {{ item }}
        </q-chip>
      </div>
  </div>
</template>

<script>
export default {
  name: 'Filtering',
  props: {
    items: {
      type: Array,
      require: true,
    },
    title: {
      type: String,
      require: true,
    },
    width: {
      type: String,
      default: '80%',
    },
    value: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      itemsStates: {},
    };
  },
  computed: {
    selectedItems() {
      return this.items.filter(item => this.itemsStates[item]);
    },
  },
  watch: {
    selectedItems(newV) {
      this.$emit('input', newV);
    },
  },
  mounted() {
    this.itemsStates = {};
    this.items.forEach(item => {
      this.itemsStates[item] = false;
    });
  },
  methods: {
    changeState(item) {
      // Create new object ensure object reactivity in vue template
      this.itemsStates = { ...this.itemsStates, [item]: !this.itemsStates[item] };
    },
  },
};
</script>

<style lang="scss" scoped>

  .link {
    border: 1px solid black;
    // border-radius: 10px;
    // cursor: pointer;
  }
</style>
