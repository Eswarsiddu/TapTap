public class IJPoint
{
    public int i;
    public int j;
    public IJPoint(int i, int j)
    {
        this.i = i;
        this.j = j;
    }

    private static int iMax;
    private static int jMax;

    private static int halfI;
    private static int halfJ;

    public static void setIJPointsData(int _iMax, int _jMax)
    {
        iMax = _iMax;
        jMax = _jMax;
        halfI = iMax / 2;
        halfJ = jMax / 2;
    }

    public static IJPoint RandomPoint()
    {
        int i = Constants.RandomInt(iMax);
        int j = Constants.RandomInt(jMax);
        return new IJPoint(i, j);
    }

    public static IJPoint[] HorizontalPoints()
    {
        IJPoint point = RandomPoint();
        IJPoint[] points = { new IJPoint(point.i, 0), new IJPoint(point.i, jMax) };
        return points;
    }

    public static IJPoint[] VerticalPoints()
    {
        IJPoint point = RandomPoint();
        IJPoint[] points = new IJPoint[2];

        points[0] = point;
        points[1] = new IJPoint(0, point.j);

        if (point.i >= halfI)
        {
            points[1].i = point.i - jMax;
        }
        else
        {
            points[1].i = point.i + jMax;
        }

        return points;
    }

    public static IJPoint[] DiagonalPoints()
    {
        IJPoint point = RandomPoint();
        IJPoint[] points = { new IJPoint(0, 0), new IJPoint(0, jMax) };
        bool isLeft = Constants.RandomBool();
        bool canLeft = true;
        bool canRight = true;
        if (point.i == 0 || point.i == iMax)
        {
            if (point.j <= halfJ)
            {
                point.j = 0;
            }
            else
            {
                point.j = jMax;
            }
        }

        if (point.i - point.j < 0 || point.i + (jMax - point.j) > iMax)
        {
            canLeft = false;
        }

        if (point.i - (jMax - point.j) < 0 || point.i + point.j > iMax)
        {
            canRight = false;
        }

        if (canLeft == false && canRight == false)
        {
            if (point.j <= halfJ)
            {
                point.j = 0;
                if (point.i <= halfI)
                {
                    isLeft = true;
                }
                else
                {
                    isLeft = false;
                }
            }
            else
            {
                point.j = jMax;
                if (point.i <= halfI)
                {
                    isLeft = false;
                }
                else
                {
                    isLeft = true;
                }
            }
        }
        else if (canLeft == true && canRight == false)
        {
            isLeft = true;
        }
        else if (canLeft == false && canRight == true)
        {
            isLeft = false;
        }

        if (isLeft)
        {
            points[0].i = point.i - point.j;
            points[1].i = point.i + (jMax - point.j);
        }
        else
        {
            points[0].i = point.i - (jMax - point.j);
            points[1].i = point.i + point.j;
        }
        return points;
    }
}