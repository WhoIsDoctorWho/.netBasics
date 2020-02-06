using System;
using System.Text.RegularExpressions;

// ax^n+bx^(n-1)+...+kx+l - format
public class Polynom {
    public int[] coefficients;
    public Polynom(string polynom)
    {
        // @todo check with regular exp
        if(isPolynomial(polynom)) {

        } //else //toCheck.Replace(" ", "");
            //string[] terms = polynom.Split(new char[] { '+' });

    }
    public Polynom(int order) {
        Random rand = new Random();
        coefficients = new int[order];
        for(int i = 0; i < order; i++) {
            coefficients[i] = rand.Next(5);
        }
    }
    public void Get() {        
        for(int i = coefficients.Length - 1; i >= 0; i--) {
            if(i != 0)
                Console.Write(coefficients[i] + "*x^" + i + " + ");
            else
                Console.Write(coefficients[i]);
        }
        Console.WriteLine();
    }
    private bool isPolynomial(string toCheck) {
        toCheck = toCheck.Replace(" ", "");
        string pattern = @"\d*[x|y|z]\^\d*|\d*$";
        Regex regular = new Regex(pattern);
        MatchCollection matches = regular.Matches(toCheck);
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
                Console.WriteLine(match.Value);
        }
        else
        {
            Console.WriteLine("Совпадений не найдено");
        }
        return true;
    }
}