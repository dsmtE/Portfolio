precision mediump float;
varying vec3 vVertexPosition;
varying vec2 vTextureCoord;

uniform vec2 uMousePosition;
uniform float uTime;

void main() {
  vec3 finalColor = vec3(0.0, 0.0, 0.0);
  float distance = distance(vec2(vVertexPosition.x, vVertexPosition.y), uMousePosition);
  finalColor.r = distance / 1.15 + cos(uTime/100.0);
  finalColor.g = abs(0.5 - distance) / 1.25;
  finalColor.b = abs(0.75 - distance) / 2.5 + 0.3;
  finalColor = mix(finalColor, vec3(1), 0.75);
  gl_FragColor = vec4(finalColor, 1.0);
}
