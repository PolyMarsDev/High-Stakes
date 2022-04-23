using UnityEngine;

namespace Utils {
	public static class Easing {
		public static float EaseInQuad( float t,  float  d) {
			float v = (t/=d)*t;
			return 1*v + 0;
		}
		public static float EaseOutQuad ( float t,   float d) => 
			-1 *(t/=d)*(t-2) + 0;
		public static float EaseInOutQuad ( float t,   float d) {
			if ((t/=d/2) < 1) return 1f/2*t*t + 0;
			return -1f/2 * ((--t)*(t-2) - 1) + 0;
		}
		public static float EaseInCubic ( float t,   float d) => 
			1*(t/=d)*t*t + 0;
		public static float EaseOutCubic ( float t,   float d) => 
			1*((t=t/d-1)*t*t + 1) + 0;
		public static float EaseInOutCubic ( float t,   float d) {
			if ((t/=d/2) < 1) return 1f/2*t*t*t + 0;
			return 1f/2*((t-=2)*t*t + 2) + 0;
		}
		public static float EaseInQuart ( float t,   float d) => 
			1*(t/=d)*t*t*t + 0;
		public static float EaseOutQuart ( float t,   float d) => 
			-1 * ((t=t/d-1)*t*t*t - 1) + 0;
		public static float EaseInOutQuart ( float t,   float d) {
			if ((t/=d/2) < 1) return 1f/2*t*t*t*t + 0;
			return -1f/2 * ((t-=2)*t*t*t - 2) + 0;
		}
		public static float EaseInQuint ( float t,   float d) => 1*(t/=d)*t*t*t*t + 0;
		public static float EaseOutQuint ( float t,   float d) => 1*((t=t/d-1)*t*t*t*t + 1) + 0;
		public static float EaseInOutQuint ( float t,   float d) {
			if ((t/=d/2) < 1) return 1f/2*t*t*t*t*t + 0;
			return 1f/2*((t-=2)*t*t*t*t + 2) + 0;
		}
		public static float EaseInSine ( float t,   float d) => 
			-1 * Mathf.Cos(t/d * (Mathf.PI/2)) + 1 + 0;
		public static float EaseOutSine ( float t,   float d) => 
			1 * Mathf.Sin(t/d * (Mathf.PI/2)) + 0;
		public static float EaseInOutSine ( float t,   float d) => 
			-1f/2 * (Mathf.Cos(Mathf.PI*t/d) - 1) + 0;
		public static float EaseInExpo ( float t,   float d) =>
			(t==0) ? 0 : 1 * Mathf.Pow(2, 10 * (t/d - 1)) + 0;
		public static float EaseOutExpo ( float t,   float d) =>
			(t==d) ? 0+1 : 1 * (-Mathf.Pow(2,-10 * t/d) + 1) + 0;
		public static float EaseInOutExpo ( float t,   float d) {
			if (t==0) return 0;
			if (t==d) return 0+1;
			if ((t/=d/2) < 1) return 1f/2 * Mathf.Pow(2, 10 * (t - 1)) + 0;
			return 1f/2 * (-Mathf.Pow(2,-10 * --t) + 2) + 0;
		}
		public static float EaseInCirc ( float t,   float d) =>
			-1 * (Mathf.Sqrt(1 - (t/=d)*t) - 1) + 0;
		public static float EaseOutCirc ( float t,   float d) =>
			1 * Mathf.Sqrt(1 - (t=t/d-1)*t) + 0;
		public static float EaseInOutCirc ( float t,   float d) {
			if ((t/=d/2) < 1) {
				return -1f/2 * (Mathf.Sqrt(1 - t*t) - 1) + 0;
			}
			return 1f/2 * (Mathf.Sqrt(1 - (t-=2)*t) + 1) + 0;
		}
		public static float EaseInElastic ( float t,   float d) {
			float s=1.70158f;float p=0;float a=1;
			if (t==0) return 0;  if ((t/=d)==1) return 0+1;  if (p!=0) p=d*.3f;
			if (a < Mathf.Abs(1)) { a=1; s=p/4; }
			else s = p/(2*Mathf.PI) * Mathf.Asin(1/a);
			return -(a*Mathf.Pow(2,10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )) + 0;
		}
		public static float EaseOutElastic ( float t,   float d) {
			float s=1.70158f;float p=0;float a=1;
			if (t==0) return 0;  if ((t/=d)==1) return 0+1;  if (p!=0) p=d*.3f;
			if (a < Mathf.Abs(1)) { a=1; s=p/4; }
			else s = p/(2*Mathf.PI) * Mathf.Asin(1/a);
			return a*Mathf.Pow(2,-10*t) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p ) + 1 + 0;
		}
		public static float EaseInOutElastic ( float t, float d) {
			float s=1.70158f;float p=0;float a=1;
			if (t==0) return 0;  if ((t/=d/2)==2) return 0+1;  if (p != 0) p=d*(.3f*1.5f);
			if (a < Mathf.Abs(1)) { a=1; s=p/4; }
			else s = p/(2*Mathf.PI) * Mathf.Asin(1/a);
			if (t < 1) return -.5f*(a*Mathf.Pow(2,10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )) + 0;
			return a*Mathf.Pow(2,-10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )*.5f + 1 + 0;
		}
		public static float EaseInBack ( float t,   float d, float s) {
			if (float.IsNaN(s)) s = 1.70158f;
			return 1*(t/=d)*t*((s+1)*t - s) + 0;
		}
		public static float EaseOutBack ( float t,   float d, float s) {
			if (float.IsNaN(s)) s = 1.70158f;
			return 1*((t=t/d-1)*t*((s+1)*t + s) + 1) + 0;
		}
		public static float EaseInOutBack ( float t,   float d, float s) {
			if (float.IsNaN(s)) s = 1.70158f;
			if ((t/=d/2) < 1) return 1f/2*(t*t*(((s*=(1.525f))+1)*t - s)) + 0;
			return 1f/2*((t-=2)*t*(((s*=(1.525f))+1)*t + s) + 2) + 0;
		}
		public static float SmoothStep(float t, float d) => (t/=d)*t*(3-2*t);
	}
}