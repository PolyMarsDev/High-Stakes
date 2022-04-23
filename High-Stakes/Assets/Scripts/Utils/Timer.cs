using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public struct Timer
	{
		public float Duration {get; private set; }
		bool paused;
		float timeLeftLagged;

		public float TimeLeft {
			get { 
				if (!paused)
					timeLeftLagged = Mathf.Max(timeLeftLagged - (Time.time - TimeLastSampled), 0);
				TimeLastSampled = Time.time;
				return timeLeftLagged;
			}
			set {
				timeLeftLagged = value;
				TimeLastSampled = Time.time;
			}
		}

		public float TimeLastSampled {get; private set;}

		public Timer(float durationSeconds, bool pausedDefault = false) {
			Duration = durationSeconds;
			timeLeftLagged = 0;
			TimeLastSampled = Time.time;
			paused = pausedDefault;
			if (!pausedDefault) Start();
		}

		public void Start() { 
			TimeLeft = Duration; 
			paused = false;
		}
		public void Pause() { 
			float left = TimeLeft; 
			paused = true;
		}
		public void UnPause() { 
			float left = TimeLeft; 
			paused = false;
		}
		public void Stop() { TimeLeft = 0; }

		public static implicit operator bool(Timer timer) => timer.TimeLeft > 0;
		public static implicit operator Timer(float durationSeconds) => new Timer(durationSeconds);
	}
}