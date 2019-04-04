using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum ObstacleType {
    CAPSULE,
    BOX,
}

[StructLayout(LayoutKind.Explicit)]
public struct ObstacleSize {
    [FieldOffset(0)]
    public Vector3 boxSize;
    [FieldOffset(0)]
    public Vector2 capsuleSize; // x is the radius, y is the height
}

public class Obstacle {
    private ObstacleType type;
    private Vector3 center;
    private ObstacleSize size;

    public Vector3 GetCenter() {
        return center;
    }

    public ObstacleSize GetSize() {
        return size;
    }

    public bool rayCircleIntersection(float px, float pz,    // ray start
                                      float dx, float dz,  // ray direction
                                      float ix, float iy,    // intersection point
                                      ref float t) { // distance along normalised ray direction to intersection
        if (type != ObstacleType.CAPSULE) {
            return false;
        }

        // Normalise the direction
        float temp = dx * dx + dz * dz;
        if (temp == 0)
            return false;

        temp = Mathf.Sqrt(temp);

        dx /= temp;
        dz /= temp;

        // get vector from line start to circle centre
        float ex = center.x - px;
        float ez = center.z - pz;

        // get squared length of e
        float e2 = ex * ex + ez * ez;

        // get squared radius
        float r2 = size.capsuleSize.x * size.capsuleSize.x;

        // determine if starting inside circle
        if (e2 < r2) {
            // if inside then reverse test direction
            dx *= -1;
            dz *= -1;
        }

        // project sphere centre onto d to get edge of a triangle
        float a = ex * dx + ez * dz;

        // squared edge length
        float a2 = a * a;

        // use pythagoras to determine intersection
        float f2 = r2 - e2 + a2;
        // f2 is now the amount of penetration into the circle squared

        // if negative then no penetration
        if (f2 < 0)
            return false;

        // calculate distance in direction d from p that the intersection occurs
        temp = a - Mathf.Sqrt(f2);

        ix = dx * temp + px;
        iy = dz * temp + pz;

        if (t != 0f)
            t = temp;

        return true;
    }

    public bool rayBoxIntersection(float px, float py,    // ray start
                                   float dx, float dy,  // ray direction
                                   float x, float y, float w, float h, // box position and size
                                   float nx, float ny,    // normal intersection
                                   float t = 0f) { // distance along ray direction to intersection
        if (type != ObstacleType.BOX) {
            return false;
        }

        return true;
    }
}
