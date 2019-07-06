﻿using System;
using UnityEngine;

namespace Tools
{
    public interface IKeyboardInput
    {
        KeyCode Key { get; }
        Action OnKey { get; set; }
        Action OnKeyDown { get; set; }
        Action OnKeyUp { get; set; }
        void StartTracking();
        void StopTracking();
    }
}