using System;

[Serializable]
public class Array2D<T> where T : struct
{
    public int x, y;
    public T[] SingleArray;

    public T this[int x, int y]
    {
        get => SingleArray[y * this.x + x];
        set => SingleArray[y * this.x + x] = value;
    }

    public Array2D(int x, int y)
    {
        this.x = x;
        this.y = y;
        SingleArray = new T[x * y];
    }

    public int Get_X_Length => x;
    public int Get_Y_Length => y;
    public int Length => SingleArray.Length;
}