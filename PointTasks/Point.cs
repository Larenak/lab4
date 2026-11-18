using System;

namespace Lab4.PointTasks;

public class Point
{
    private double _x = 0;
    private double _y = 0;

    public Point() : this(0, 0)
    {
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point(Point other) : this(other.X, other.Y)
    {
    }

    public double X
    {
        get
        {
            return _x;
        }
        set
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Нужна конечная координата.");
            }

            _x = value;
        }
    }

    public double Y
    {
        get
        {
            return _y;
        }
        set
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Нужна конечная координата.");
            }

            _y = value;
        }
    }

    public double DistanceTo(Point other)
    {
        return double.Hypot(_x - other._x, _y - other._y);
    }

    public override string ToString()
    {
        return $"({_x}; {_y})";
    }

    public static Point operator ++(Point point)
    {
        return new Point(point._x + 1, point._y);
    }

    public static Point operator --(Point point)
    {
        return new Point(point._x - 1, point._y);
    }

    public static explicit operator int(Point point)
    {
        return checked((int)point._x);
    }

    public static implicit operator double(Point point)
    {
        return point._y;
    }

    public static double operator +(Point left, Point right)
    {
        return left.DistanceTo(right);
    }

    public static Point operator +(Point point, int number)
    {
        return new Point(point._x + number, point._y);
    }

    public static Point operator +(int number, Point point)
    {
        return point + number;
    }
}
