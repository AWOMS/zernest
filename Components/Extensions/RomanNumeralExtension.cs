using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWOMS.Zernest.Components.Extensions;

public static class RomanNumeralExtension
{
    public static string ToRoman(this int value)
    {
        // create 2-dimensional array, each inner array containing 
        // roman numeral representations of 1-9 in each respective 
        // place (ones, tens, hundreds, etc...currently this handles
        // integers from 1-3999, but could be easily extended)
        var romanNumerals = new string[][] {
            new string[] {"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"}, // ones
            new string[] {"", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"}, // tens
            new string[] {"", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"}, // hundreds
            new string[] {"", "M", "MM", "MMM"} // thousands
        };

        // split integer string into array and reverse array
        var digits = value.ToString().ToCharArray();
        Array.Reverse(digits);

        // starting with the highest place (for 3046, it would be the thousands 
        // place, or 3), get the roman numeral representation for that place 
        // and append it to the final roman numeral string
        string romanNumeral = "";
        var i = digits.Length;
        while (i-- > 0) {
            romanNumeral += romanNumerals[ i ][ digits[i]-'0' ];
        }

        return romanNumeral;
    }
}