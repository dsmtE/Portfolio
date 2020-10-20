<template>
  <div id="canvas"></div>   <!-- div that will hold our WebGL canvas -->
</template>

<script>
import { Curtains, Plane, Vec2 } from 'curtainsjs';
// import { throttle } from 'quasar';

export default {
  name: 'CurtainsWrapper',
  props: {
    planes: {
      type: Array,
      default: () => [],
      validator: array => array.every(
        p => p.DOMElement !== undefined
        && p.vs !== undefined
        && p.fs !== undefined
        && p.uniforms !== undefined,
      ),
    },
    mouseSmoothness: {
      type: Number,
      default: 0.5,
    },
  },
  data() {
    return {
      canvas: undefined,
      curtainsPlanes: undefined,
      mousePosition: undefined,
      mouseLastPosition: undefined,
      moveStrength: undefined,
      uniforms: {
        time: {
          name: 'uTime',
          type: '1f',
          value: 0,
        },
        mousePosition: {
          name: 'uMousePosition',
          type: '2f', // again an array of floats
          value: new Vec2(),
        },
        moveStrength: {
          name: 'uMoveStrength',
          type: '1f',
          value: this.moveStrength,
        },
      },
    };
  },
  mounted() {
    this.curtains = new Curtains({
      container: 'canvas',
      pixelRatio: Math.min(1.5, window.devicePixelRatio), // limit pixel ratio for performance
    });

    // handling errors
    this.curtains.onError(() => {
      // we will add a class to the document body to display original canvas
      // document.body.classList.add('no-curtains');
    }).onContextLost(() => {
      // on context lost, try to restore the context
      this.curtains.restoreContext();
    });
    this.curtainsPlanes = this.planes.map(
      p => new Plane(this.curtains, p.DOMElement, this.getPlaneParams(p)),
    );
    this.curtainsPlanes.forEach(this.setupPlane);
  },
  methods: {
    setupPlane(plane) {
      const planeUniforms = Object.keys(plane.uniforms);
      // if (planeUniforms.some(u => !Object.keys(this.uniforms).includes(u))) {
      //   console.log('Some curtains plane contains unknown uniforms!');
      // }

      if (planeUniforms.includes('mousePosition')) {
        this.mousePosition = new Vec2();
        this.mouseLastPosition = new Vec2();
      }

      // const planeBoundingRect = plane.getBoundingRect(); // get our plane dimensions

      // size our canvas
      // we are dividing it by the pixel ratio value to gain performance
      // simpleCanvas.width = planeBoundingRect.width / curtains.pixelRatio;
      // simpleCanvas.height = planeBoundingRect.height / curtains.pixelRatio;

      plane.onReady(() => {
        // document.body.classList.add("curtains-ready"); // display the button

        // set a fov of 40 to reduce perspective
        plane.setPerspective(40);

        if (planeUniforms.includes('mousePosition')) { // add mousePosition listener if needed
          window.addEventListener('mousemove', e => this.handleMovement(e, plane));
          window.addEventListener('touchmove', e => this.handleMovement(e, plane), { passive: true });
        }
      }).onRender(() => {
        if (planeUniforms.includes('time')) {
          plane.uniforms.time.value += 1; // increment our time uniforn
        }
      }).onAfterResize(() => {
        // get our plane dimensions
        // const planeBoundingRect = simplePlane.getBoundingRect();

        // size our canvas
        // we are dividing it by the pixel ratio value to gain performance
        // simpleCanvas.width = planeBoundingRect.width / curtains.pixelRatio;
        // simpleCanvas.height = planeBoundingRect.height / curtains.pixelRatio;
      });
    },
    getPlaneUniforms(uniformsList) {
      return Object.fromEntries(uniformsList.map(uniform => [uniform, this.uniforms[uniform]]));
    },
    getPlaneParams(plane) {
      return {
        vertexShader: plane.vs,
        fragmentShader: plane.fs,
        uniforms: this.getPlaneUniforms(plane.uniforms),
        // ...plane.othersParams,
      };
    },
    handleMovement(e, plane) {
      const planeUniforms = Object.keys(plane.uniforms);
      this.mouseLastPosition.copy(this.mousePosition); // update mouse last pos

      const mouse = new Vec2();

      if (e.targetTouches) { // touch event
        mouse.set(e.targetTouches[0].clientX, e.targetTouches[0].clientY);
      } else { // mouse event
        mouse.set(e.clientX, e.clientY);
      }

      this.mousePosition.set( // lerp the mouse position a bit to smoothen the overall effect
        this.curtains.lerp(this.mousePosition.x, mouse.x, this.mouseSmoothness),
        this.curtains.lerp(this.mousePosition.y, mouse.y, this.mouseSmoothness),
      );

      // convert our mouse/touch position to coordinates
      // relative to the vertices of the plane and update our uniform
      plane.uniforms.mousePosition.value = plane.mouseToPlaneCoords(this.mousePosition);

      if (planeUniforms.includes('moveStrength')) {
        const deltaMousePosition = this.mousePosition - this.mouseLastPosition;
        this.moveStrength = Math.sqrt(deltaMousePosition.dot(deltaMousePosition));
        this.uniforms.moveStrength.value = this.moveStrength;
      }
    },
  },
};
</script>

<style lang="scss" scoped>
  #canvas {
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: -100;
  }

  // #canvas {
  //   height: 100vh;
  //   width: 100vw;
  //   opacity: 1;
  //   transition: opacity 0.5s ease-in;
  //   }

</style>
