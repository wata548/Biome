using System.Collections.Generic;
using System;
using Random = System.Random;

public class PerLinNoise
{

    public class Vector2(float pX, float pY) {
        public float X = pX;
        public float Y = pY;
        
        public static Vector2 operator*(Vector2 lhs, float rhs) =>
            new(lhs.X * rhs, lhs.Y * rhs);
    } 
    
   //==================================================||Fields 
    private readonly int SEED = 3667;
    
   //==================================================||Constructors 
    public PerLinNoise(int pSeed = 12234324) {
        SEED = pSeed;
    }

   //==================================================||Mehods 
    public float Get(float pX, float pY, int pOctave = 1) =>
        Get(new Vector2(pX, pY), pOctave);

    public float Get(Vector2 pPoint, int pOctave = 1) {
        
        float result = 0;
        int frequency = 1;

        while (pOctave-- > 0) {
            result += PerlinNoise2D(pPoint * frequency) / frequency;
            frequency <<= 1;
        }

        return result;
    }

    private float PerlinNoise2D(Vector2 pPoint) {

        var floor = new Func<float, int>(v => (int)Math.Floor(v))!;
        var grid = new Coord(floor(pPoint.X), floor(pPoint.Y));

        var     interval    = SetInterval();

        float leftUp = RandomDotProduction2D(grid.X, grid.Y, pPoint);
        float rightUp = RandomDotProduction2D(grid.X + 1, grid.Y, pPoint);
        float leftDown = RandomDotProduction2D(grid.X, grid.Y + 1, pPoint);
        float rightDown = RandomDotProduction2D(grid.X + 1, grid.Y + 1, pPoint);

        float   lerpX1      = Lerp(interval.Item1, leftUp,      rightUp);
        float   lerpX2      = Lerp(interval.Item1, leftDown,    rightDown);
        float   result      = Lerp(interval.Item2, lerpX1,      lerpX2);

        return result;

        (float, float) SetInterval() {

            float intervalX = Smooth(pPoint.X - grid.X);
            float intervalY = Smooth(pPoint.Y - grid.Y);

            return (intervalX, intervalY);
        }

        float RandomDotProduction2D(int gridX, int gridY, Vector2 coor) {

            float degree = (GetSeed() % 10000 / 10000f) * 2 * (float)Math.PI;
            float deltaX = coor.X - gridX;
            float deltaY = coor.Y - gridY;

            if(deltaX == 0 && deltaY == 0) {

                deltaX = 0.01f;
            }

            float dotProductionX = deltaX * (float)Math.Cos(degree);
            float dotProductionY = deltaY * (float)Math.Sin(degree);

            return dotProductionX + dotProductionY;

            int GetSeed() {

                int seed = 0;

                int[] RandomMultiple = { 13453, 8535};
                int[] RandomIncrese = { 7442243, 2364257};

                seed ^= gridX * RandomMultiple[0] + RandomIncrese[0];
                seed ^= gridY * RandomMultiple[1] + RandomIncrese[1];
                seed += SEED;

                return seed;
            }
        }
    }

    private float Smooth(float pX) {
        return pX * pX * (3 - 2 * pX);
    }

    private float Lerp(float pT, float pX1, float pX2) {

        return (1 - pT) * pX1 + pT * pX2;
    }
}