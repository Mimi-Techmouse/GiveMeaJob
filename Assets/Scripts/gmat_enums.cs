using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_enums : MonoBehaviour {

	public enum TIMEFRAME {
		NONE,
		EVERY_DAY,
		ANYTIME_ON_DAY,
		ANYTIME_AFTER,
		EVERY_WEEK,
		THIS_WEEK,
		THIS_MONTH,
		EVERY_YEAR,
		ANYTIME,
		MORNING_OF,
		AFTERNOON_OF,
		EVENING_OF,
		BETWEEN,
		EVERY_DAY_TIME
	}

	public enum MONTH {
		NONE,
		JANUARY,
		FEBRUARY,
		MARCH,
		APRIL,
		MAY,
		JUNE,
		JULY,
		AUGUST,
		SEPTEMBER,
		OCTOBER,
		NOVEMBER,
		DECEMBER
	}

	public enum DAY {
		NONE,
		SUNDAY,
		MONDAY,
		TUESDAY,
		WEDNESDAY,
		THURSDAY,
		FRIDAY,
		SATURDAY
	}
}
