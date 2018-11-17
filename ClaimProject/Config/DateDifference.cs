using System;

namespace ClaimProject.Config
{
    public class DateDifference
    {
        private DateTime _FromDate;
        private DateTime _ToDate;
        private int _Year = 0;
        private int _Month = 0;
        private int _Day = 0;

        public int Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        public int Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        public int Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        /// <summary>
        /// defining Number of days in month; index 0=> january and 11=> December
        /// february contain either 28 or 29 days, that's why here value is -1
        /// which wil be calculate later.
        /// </summary>
        private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public DateDifference(DateTime argFromDate)
        {
            _ToDate = DateTime.Now;
            _FromDate = argFromDate;
            CalcDifferenceDate();
        }

        public DateDifference(DateTime argFromDate, DateTime argToDate)
        {
            _ToDate = argToDate;
            _FromDate = argFromDate;
            CalcDifferenceDate();
        }

        private void SwapDate(ref DateTime LeftDate, ref DateTime RightDate)
        {
            DateTime tempDate;
            if (LeftDate > RightDate)
            {
                tempDate = LeftDate;
                LeftDate = RightDate;
                RightDate = tempDate;
            }
        }

        private void CalcDifferenceDate()
        {

            SwapDate(ref _FromDate, ref _ToDate);

            TimeSpan ts = _ToDate - _FromDate;
            DateTime Age = DateTime.MinValue.AddDays(ts.Days);

            this.Day = Age.Day-1;
            this.Month = Age.Month-1;
            this.Year = Age.Year-1;
        }

        public string ToString(string argYearUnit, string argMonthUnit, string argDayUnit)
        {
            string retStr = string.Empty;
            if (this.Year > 0)
                retStr = retStr + string.Format("{0} {1} ", this.Year.ToString("#,##0"), argYearUnit);
            if (this.Month > 0)
                retStr = retStr + string.Format("{0} {1} ", this.Month.ToString("#,##0"), argMonthUnit);
            if (this.Day > 0)
                retStr = retStr + string.Format("{0} {1} ", this.Day.ToString("#,##0"), argDayUnit);
            return retStr.Trim();
        }

        public override string ToString()
        {
            return this.ToString(" ปี ", " เดือน ", " วัน");
        }
    }
}