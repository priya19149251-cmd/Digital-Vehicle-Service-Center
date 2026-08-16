using System;
interface IService
{
    void ShowService();
}
abstract class Person
{
    public string name;
    public string phone;
    public Person(string name, string phone)
    {
        this.name = name;
        this.phone = phone;
    }

    public virtual void ShowInfo()
    {
        Console.WriteLine($"Name: {name}");
    }
}

class Customer : Person
{
    public Customer(string name, string phone):base(name, phone)
    {
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"Customer: {name}");
        Console.WriteLine($"Phone: {phone}");
    }
}

class Mechanic : Person
{
    public string skill;

    public Mechanic(string name, string phone, string skill):base(name, phone)
    {
        this.skill = skill;
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"Mechanic: {name}");
        Console.WriteLine($"Phone: {phone}");
        Console.WriteLine($"Skill: {skill}");
    }
}

class Vehicle
{
    public string brand;
    public string model;
    public string number;

    public Vehicle(string brand, string model, string number)
    {
        this.brand = brand;
        this.model = model;
        this.number = number;
    }
    public Vehicle(string brand, string model)
        : this(brand, model, "Not Given")
    {
    }
    public Vehicle(Vehicle v)
    {
        brand = v.brand;
        model = v.model;
        number = v.number;
    }

    public void ShowVehicle()
    {
        Console.WriteLine($"Vehicle: {brand} {model} | Number: {number}");
    }
}

class ServiceJob : IService
{
    public int jobId;
    public Vehicle vehicle;
    public Mechanic mechanic;
    public string status;

    public string[] Services = new string[5];

    public double LaborCost;
    public double PartsCost;
    public ServiceJob(int id, Vehicle v)
    {
        jobId = id;
        vehicle = v;
        status = "Pending";
    }
    public void AddService(string service)
    {
        Services[0] = service;
    }

    public void AddService(string service, double cost)
    {
        Services[1] = service;
        LaborCost = LaborCost + cost;
    }

    public void AddPart(double cost)
    {
        PartsCost = PartsCost + cost;
    }

    public void AssignMechanic(Mechanic m)
    {
        mechanic = m;
        status = "In Progress";
    }

    public void CompleteJob()
    {
        status = "Completed";
    }

    public double TotalCost()
    {
        return LaborCost + PartsCost;
    }
    public static ServiceJob operator +(ServiceJob job, double cost)
    {
        job.PartsCost = job.PartsCost + cost;
        return job;
    }
    public void ShowService()
    {
        Console.WriteLine($"Service Job ID: {jobId}");
    }
    public void GenerateInvoice()
    {
        Console.WriteLine($"Job ID: {jobId}");
        Console.WriteLine($"Vehicle: {vehicle.brand} {vehicle.model}");
        Console.WriteLine($"Mechanic: {mechanic.name}");
        Console.WriteLine($"Status: {status}");

        Console.WriteLine($"\nServices:");

        Console.WriteLine($"Service 1: {Services[0]}");
        Console.WriteLine($"Service 2: {Services[1]}");

        Console.WriteLine($"\nLabor Cost: {LaborCost}");
        Console.WriteLine($"Parts Cost: {PartsCost}");
        Console.WriteLine($"Total Cost: {TotalCost()}");

    }
}
class ServiceCenter
{
    public string CenterName;

    public ServiceCenter(string name)
    {
        CenterName = name;
    }
    public void RegisterCustomer(Customer customer)
    {
        Console.WriteLine($"Customer registered: {customer.name}");
    }
    public void RegisterCustomer(string name, string phone)
    {
        Console.WriteLine($"Customer registered: {name}");
        Console.WriteLine($"Phone: {phone}");
    }
    public void RegisterVehicle(Vehicle vehicle)
    {
        Console.WriteLine($"Vehicle registered: {vehicle.brand} {vehicle.model}");
    }
    public static void GenerateReport(ServiceJob job)
    {
        Console.WriteLine($"Job ID: {job.jobId}");
        Console.WriteLine($"Status: {job.status}");
        Console.WriteLine($"Revenue: {job.TotalCost()}");
    }
}

class Program
{
    static void Main()
    {
        ServiceCenter center =new ServiceCenter("Spider Man Service Center");
        Customer customer =new Customer("A", "01700000000");
        center.RegisterCustomer(customer);
        customer.ShowInfo();
        Vehicle car =new Vehicle("Toyota","Corolla","DHA-1234");
        center.RegisterVehicle(car);
        car.ShowVehicle();
        Vehicle copiedCar =new Vehicle(car);
        copiedCar.ShowVehicle();
        Mechanic mechanic = new Mechanic("Rahim", "01800000000","Engine Specialist");
        mechanic.ShowInfo();
        ServiceJob job =new ServiceJob(101, car);
        job.AssignMechanic(mechanic);
        job.AddService("Engine Check",1000);
        job.AddService("Oil Change", 500);
        job.AddService("Brake Check");
        job.AddPart(1500);
        job = job + 500;
        job.CompleteJob();
        job.ShowService();
        ServiceCenter.GenerateReport(job);  
    }
}
