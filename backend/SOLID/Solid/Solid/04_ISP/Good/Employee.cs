namespace Solid._04_ISP.Good;

public interface ISalaryCalculator
{
    void CalculateSalary();
}

public interface IBenefitsCalculator
{
    void CalculateBenefits();
}

public class FullTimeEmployee : ISalaryCalculator, IBenefitsCalculator
{
    public void CalculateSalary()
    {
        Console.WriteLine("Salary Calculated");
    }
    public void CalculateBenefits()
    {
        throw new NotImplementedException();
    }
}

public class ContractEmployee : ISalaryCalculator
{
    public void CalculateSalary()
    {
        Console.WriteLine("Salary Calculated");
    }
}
