using System;
using System.Collections.Generic;

namespace EmergencyResponseSimulation
{
    // Abstract base class
    abstract class EmergencyUnit
    {
        public string Name { get; set; }
        public int Speed { get; set; }

        public EmergencyUnit(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public abstract bool CanHandle(string incidentType);
        public abstract void RespondToIncident(Incident incident);
    }

    // Police Unit - Handles "Crime"
    class Police : EmergencyUnit
    {
        public Police(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Crime";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is handling a crime at {incident.Location}.");
        }
    }

    // Firefighter Unit - Handles "Fire"
    class Firefighter : EmergencyUnit
    {
        public Firefighter(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Fire";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is extinguishing fire at {incident.Location}.");
        }
    }

    // Ambulance Unit - Handles "Medical"
    class Ambulance : EmergencyUnit
    {
        public Ambulance(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Medical";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is treating patients at {incident.Location}.");
        }
    }

    // Incident class
    class Incident
    {
        public string Type { get; set; }
        public string Location { get; set; }

        public Incident(string type, string location)
        {
            Type = type;
            Location = location;
        }
    }

    // Program
    class Program
    {
        static void Main(string[] args)
        {
            List<EmergencyUnit> units = new List<EmergencyUnit>
            {
                new Police("Police Unit 1", 80),
                new Firefighter("Firefighter Unit 1", 70),
                new Ambulance("Ambulance Unit 1", 90)
            };

            string[] incidentTypes = { "Fire", "Crime", "Medical" };
            string[] locations = { "Central Park", "Downtown", "City Hall", "Airport", "Museum" };

            Random random = new Random();
            int score = 0;

            for (int round = 1; round <= 5; round++)
            {
                Console.WriteLine($"\n--- Turn {round} ---");
                string incidentType = incidentTypes[random.Next(incidentTypes.Length)];
                string location = locations[random.Next(locations.Length)];
                Incident incident = new Incident(incidentType, location);

                Console.WriteLine($"Incident: {incident.Type} at {incident.Location}");

                bool handled = false;

                foreach (var unit in units)
                {
                    if (unit.CanHandle(incident.Type))
                    {
                        unit.RespondToIncident(incident);
                        score += 10;
                        handled = true;
                        break;
                    }
                }

                if (!handled)
                {
                    Console.WriteLine("No available unit to handle this incident.");
                    score -= 5;
                }

                Console.WriteLine($"Current Score: {score}");
            }

            Console.WriteLine($"\n--- Simulation Complete ---");
            Console.WriteLine($"Final Score: {score}");
        }
    }
}
