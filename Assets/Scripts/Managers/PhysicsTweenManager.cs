using System;
using System.Collections.Generic;
using Base;

namespace Managers
{
    public class PhysicsTweenManager
    {
        private static PhysicsTweenManager _instance;
        private readonly List<PhysicsTween> _tweenList = new ();
        private readonly List<PhysicsTween> _deActiveTweenList = new ();

        public static PhysicsTweenManager Instance
        {
            get { return _instance ??= new PhysicsTweenManager(); }
        }

        public void PlayTween(PhysicsTween tween)
        {
            tween.isActive = true;
            tween.Start();
            _tweenList.Add(tween);
        }

        public void StopTween(PhysicsTween tween)
        {
            tween.isActive = false;
        }

        public void Tick()
        {
            _deActiveTweenList.Clear();
            foreach (var tween in _tweenList)
            {
                if (!tween.isPause) tween.Update();
                if (!tween.isActive) _deActiveTweenList.Add(tween);
            }

            foreach (var tween in _deActiveTweenList)
            {
                tween.End();
                _tweenList.Remove(tween);
            }
        }
    }
}