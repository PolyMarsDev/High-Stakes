using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Utils {
	public class Calc {
		static System.Random rand;
		public static System.Random Random {
			get {
				if (rand == null) rand = new System.Random();
				return rand;
			}
		}

		public static float RandomRange(float a, float b) 
			=> (float) Random.NextDouble() * (b-a) + a;

		public static int RandomRange(int a, int b)
			=> (int) (Random.Next(a,b));

		public static float Approach(float cur, float target, float distance) {
			float amt = Mathf.Clamp(target - cur, -distance, distance);
			cur += amt;
			if (Mathf.Approximately(0, cur - target))
				cur = target;
			return cur;
		}

		public static float Damp(float a, float b, float lamda, float dt) {
			return Mathf.Lerp(a, b, 1 - Mathf.Exp(-lamda * dt));
		}

		// public static Vector2 Damp(Vector2 a, Vector2 b, float lamda, float dt) {
		// 	return Vector2.Lerp(a,b,1-Mathf.Exp(-lamda*dt));
		// } 

		public static Vector3 Damp(Vector3 a, Vector3 b, float lamda, float dt) {
			return Vector3.Lerp(a, b, 1 - Mathf.Exp(-lamda * dt));
		}

		public static bool Approximately(float a, float b, float threshold = 1e-9f) {
			Assert.IsTrue(threshold >= 0);
			return (a > b ? a - b : b - a) <= threshold;
		}
		public static float Closest(float origin, float a, float b) {
			return Mathf.Abs(origin - a) > Mathf.Abs(origin - b) ? a : b;
		}


		public static Vector2 Rotate(Vector2 root, float rad) {
			return new Vector2(
				root.x * Mathf.Cos(rad) - root.y * Mathf.Sin(rad),
				root.x * Mathf.Sin(rad) + root.y * Mathf.Cos(rad)
			);
		}

		public static float Remap(float t, float a, float b, float c, float d) {
			return (t - a) / (b - a) * (d - c) + c;
		}

		public static float Remap_Clamp(float t, float a, float b, float c, float d) {
			return Mathf.Clamp((t - a) / (b - a) * (d - c) + c, c, d);
		}

		public static Quaternion ToQuat(Vector2 vec) {
			return Quaternion.Euler(0, 0, -Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg);
		}

		public static float HeightToVelocity(float height) => height > 0 ? Mathf.Sqrt(Mathf.Abs(2 * height * Physics2D.gravity.y)) : 0;

		public static LayerMask GetPhysicsLayerMask(int currentLayer) {
			int finalMask = 0;
			for (int i = 0; i < 32; i++) {
				if (!Physics.GetIgnoreLayerCollision(currentLayer, i)) finalMask = finalMask | (1 << i);
			}
			return finalMask;
		}
	}
}