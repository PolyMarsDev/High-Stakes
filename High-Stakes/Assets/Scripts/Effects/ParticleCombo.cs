using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Yarn.Unity;

namespace Effects {
	public class ParticleCombo : MonoBehaviour {
		List<ParticleSystem> burstSystems;

		void Awake() {
			RegisterSystem();
		}
		public void RegisterSystem() {
			burstSystems = new List<ParticleSystem>();
			foreach (Transform child in transform) {
				ParticleSystem system = child.GetComponent<ParticleSystem>();
				if (system != null) burstSystems.Add(system);
			}
		}
		public void Activate() {
			if (burstSystems == null) RegisterSystem();

			foreach (ParticleSystem system in burstSystems) {
				system.Clear();
				system.Stop();
				system.Play();
			}
		}
		public void Stop() {
			if (burstSystems == null) RegisterSystem();

			foreach (ParticleSystem system in burstSystems) {
				system.Clear();
				system.Stop();
			}
		}
		public void StopProducing() {
			if (burstSystems == null) RegisterSystem();

			foreach (ParticleSystem system in burstSystems) {
				system.Stop();
			}
		}
	}
}
