<template>
  <div class="cursor fullHeight">
    <div :v-show="showOutside" class="outside" :style="outsideStyle" ref="outside"></div>
    <div :v-show="showInside" class="inside" :style="insideStyle" ref="inside"></div>
  </div>
</template>

<script>
import { throttle } from 'quasar';

export default {
  name: 'CircleCursor',
  props: {
    targets: {
      type: Array,
      default: () => [],
    },
    insideColor: {
      type: String,
      default: '#333',
    },
    outsideColor: {
      type: String,
      default: '#33333311',
    },
    colorHover: {
      type: String,
      default: null,
    },
    borderColor: {
      type: String,
      default: '#333',
    },
    outsideSize: {
      type: Number,
      default: 60,
    },
    insideSize: {
      type: Number,
      default: 5,
    },
    insideDelay: {
      type: Number,
      default: 0.15,
    },
    outsideDelay: {
      type: Number,
      default: 0.25,
    },
    showOutside: {
      type: Boolean,
      default: false,
    },
    showInside: {
      type: Boolean,
      default: false,
    },
    scaleHoverFactor: {
      type: Number,
      default: 1.2,
    },
  },
  data() {
    return {
      mousePos: {
        x: null,
        y: null,
      },
      hover: false,
    };
  },
  computed: {
    cursorPos() {
      return {
        x: this.mousePos.x,
        y: this.mousePos.y,
      };
    },
    insideStyle() {
      return {
        width: `${this.insideSize}px`,
        height: `${this.insideSize}px`,
        transform: `translate(${this.cursorPos.x - this.insideSize / 2}px, ${this.cursorPos.y - this.insideSize / 2}px) scale(${this.scaleHoverFactor})`,
        transition: `transform ${this.insideDelay}s ease-out`,
        'background-color': this.insideColor,
      };
    },
    outsideStyle() {
      return {
        width: `${this.outsideSize}px`,
        height: `${this.outsideSize}px`,
        transform: `translate(${this.cursorPos.x - this.outsideSize / 2}px, ${this.cursorPos.y - this.outsideSize / 2}px) scale(${this.scaleHoverFactor})`,
        transition: `transform ${this.outsideDelay}s ease-out`,
        'background-color': this.outsideColor,
      };
    },
  },
  methods: {
    update(e) {
      // update mousePos
      this.mousePos = this.getMousePos(e);

      // update hoverProperty
      this.hover = this.targets.length > 0
        && (this.targets.includes(e.target.tagName.toLowerCase())
        || this.targets.includes(e.target.className.toLowerCase()));
    },
    getMousePos(e) {
      return {
        x: e.clientX,
        y: e.clientY,
      };
    },
  },
  mounted() {
    window.addEventListener('mousemove', throttle(this.update, 20));
  },
};
</script>

<style lang="scss" scoped>
  .cursor {
    cursor: none;
    pointer-events: none;
    position: fixed;
    top: 0;
    left: 0;
    z-index: 100;

    .inside, .outside {
      position: fixed;
      top: 0;
      left: 0;
      pointer-events: none;
      user-select: none;
      z-index: 101;
      border-radius: 100%;
      overflow: hidden;
      transform-origin: center;
    }
  }
</style>
