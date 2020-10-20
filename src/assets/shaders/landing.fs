precision mediump float;
varying vec3 vVertexPosition;
varying vec2 vTextureCoord;

uniform vec2 uMousePosition;
uniform float uTime;

// ----- easingFunct -----//
float quadin(float t) { return t*t;}
float quadOut(float t) { return -t*(t-2.0);}
float cubicIn(float t) { return t*t*t;}
float cubicOut(float t) { return -t*t*t+1.0;}
float circleOut(float t) { return pow(1.0-(1.0-t)*(1.0-t), 0.5); }
float circleIn(float t) { return 1.0- pow(1.0-t*t, 0.5); }
float gauss(float t, float s) { return exp(-(t*t*s*s)/2.); }
// sub interpolation used in smoothstep
float hermiteInter(float t) { return t * t * (3.0 - 2.0 * t); }
vec2 hermiteInter(vec2 t) { return t * t * (3.0 - 2.0 * t); }

// ----- UsefulConstants ----- //
#define PI  3.14159265358979323846264338327

// ----- Useful functions ----- //
#define rot2(a) mat2(cos(a), -sin(a), sin(a), cos(a))
float max2(vec2 v) { return max(v.x , v.y); }
float max3(vec3 v) { return max(max(v.x , v.y), v.z); }
float dot2(vec2 v) { return dot(v,v); }
float cro(vec2 a,vec2 b) { return a.x*b.y - a.y*b.x; }
float map(float a, float b, float t) {return a + t * (b - a); } // considering that t is in [0-1]
float mult2(vec2 v) { return v.x*v.y; }
float mult3(vec3 v) { return v.x*v.y*v.z; }

// ----- Noise stuff ----- //
// source: https://www.shadertoy.com/view/4djSRW
float hash11(float p)
{
    p = fract(p * .1031);
    p *= p + 33.33;
    p *= 2.*p;
    return fract(p);
}

vec2 hash21(float p)
{
	vec3 p3 = fract(vec3(p) * vec3(.1031, .1030, .0973));
	p3 += dot(p3, p3.yzx + 33.33);
    return fract((p3.xx+p3.yz)*p3.zy);

}

vec2 hash22(vec2 p)
{
	vec3 p3 = fract(vec3(p.xyx) * vec3(.1031, .1030, .0973));
    p3 += dot(p3, p3.yzx+33.33);
    return fract((p3.xx+p3.yz)*p3.zy);

}

float hash12(vec2 p)
{
	vec3 p3  = fract(vec3(p.xyx) * .1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return fract((p3.x + p3.y) * p3.z);
}

float perlinNoise(vec2 xy) {
	vec2 lv = hermiteInter(fract(xy));
	vec2 id = floor(xy);

	float a = hash12(id+vec2(0.,0.));
	float b = hash12(id+vec2(1.,0.));
	float c = hash12(id+vec2(0.,1.));
	float d = hash12(id+vec2(1.,1.));
	float x1 = mix(a, b, lv.x);
	float x2 = mix(c, d, lv.x);
	return mix(x1, x2, lv.y);
}

float perlinNoise(float x) {
	float lv = fract(x);
	float id = floor(x);

	float a = hash11(id);
	float b = hash11(id+1.0);

	return mix(a, b, lv);
}

float fbm (vec2 x, float H, int octaves) {
	float G = exp2(-H); // 2^x
	float v = 0.;
	float f = 1.;
	float amp = 1.;
	float aSum = 1.;

    vec2 shift = vec2(100.0);
    float a = 0.6;

	for ( int i=0; i < 20; ++i) {
		if (i >= octaves) { break; } // use break and constant for loop limit for webGL
		v += amp * perlinNoise(f*x);
		f *= 2.;
		amp *= G;
		aSum += amp;
		// Rotate and shift to reduce axial bias
		x = rot2(a) * x + shift;
	}
	return v / aSum;
}

vec2 fbm2(vec2 p, vec2 shift, float H, int octaves) {
	return vec2( fbm(p, H, octaves), fbm(p+shift, H, octaves) );
}

vec3 hsl2rgb(vec3 c) {
    vec3 rgb = clamp( abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0, 0.0, 1.0 );
    return c.z + c.y * (rgb-0.5)*(1.0-abs(2.0*c.z-1.0));
}


void mainImage(out vec4 fragColor, in vec2 uv) {

    uv /= 2.;
    uv.x *= 1.5;// fix screen ration by hand

    uv /= 1.5; // zoom

    vec3 col =  vec3(1.0);

    for (float i = 0.0; i < 100.0; ++i) {
      vec2 pt = fract(hash21(i) + hash21(i*10.) * uTime *0.0001) - 0.5;
      col *= smoothstep(0.0005, 0.0008, length(pt - uv) );
    }

    fragColor = vec4(col, 1.0);
}

void main()
{
	mainImage(gl_FragColor, vVertexPosition.xy);
}
