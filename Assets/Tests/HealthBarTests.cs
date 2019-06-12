using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HealthBarTests
    {
        [UnityTest]
        public IEnumerator ResizeHealthBar_TakeDamage()
        {
            EnemyGuardNav enemy = EnemyController.instance.GetEnemies()[0].GetComponent<EnemyGuardNav>();
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
