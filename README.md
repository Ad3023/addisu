
using System;
using System.Collections.Generic;

namespace EmergencyResponseGame
{
    // Base class representing any emergency responder
    abstract class EmergencyUnit
    {
        public string UnitName { get; set; }
        public int ResponseSpeed { get; set; }

        public EmergencyUnit(string unitName, int responseSpeed)
        {
            UnitName = unitName;
            ResponseSpeed = responseSpeed;
        }

        // Determines if this unit can respond to a given type of incident
        public abstract bool CanRespondTo(string incidentType);

        // Handles the logic for how this unit responds to an incident
        public abstract void HandleIncident(Incident incident);
    }

    // Handles crimes
    class PoliceUnit : EmergencyUnit
    {
        public PoliceUnit(string unitName, int responseSpeed) : base(unitName, responseSpeed) { }

        public override bool CanRespondTo(string incidentType)
        {
            return incidentType == "Crime";
        }

        public override void HandleIncident(Incident incident)
        {
            Console.WriteLine($"{UnitName} is handling a crime at {incident.Location}.");
        }
    }

    // Handles fires
    class FirefighterUnit : EmergencyUnit
    {
        public FirefighterUnit(string unitName, int responseSpeed) : base(unitName, responseSpeed) { }

        public override bool CanRespondTo(string incidentType)
        {
            return incidentType == "Fire";
        }

        public override void HandleIncident(Incident incident)
        {
            Console.WriteLine($"{UnitName} is extinguishing a fire at {incident.Location}.");
        }
    }

    // Handles medical emergencies
    class AmbulanceUnit : EmergencyUnit
    {
        public AmbulanceUnit(string unitName, int responseSpeed) : base(unitName, responseSpeed) { }

        public override bool CanRespondTo(string incidentType)
        {
            return incidentType == "Medical";
        }

        public override void HandleIncident(Incident incident)
        {
            Console.WriteLine($"{UnitName} is treating patients at {incident.Location}.");
        }
    }

    // Describes an emergency incident
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

    // Main game logic
    class Program
    {
        static void Main(string[] args)
        {
            // Create emergency response units
            List<EmergencyUnit> responseUnits = new List<EmergencyUnit>
            {
                new PoliceUnit("Police Unit 1", 80),
                new FirefighterUnit("Firefighter Unit 1", 70),
                new AmbulanceUnit("Ambulance Unit 1", 90)
            };

            // Define possible types of incidents and city locations
            string[] possibleIncidents = { "Fire", "Crime", "Medical" };
            string[] cityLocations = { "Central Park", "Downtown", "City Hall", "Airport", "Museum" };

            Random rng = new Random();
            int playerScore = 0;

            // Run simulation for 5 rounds
            for (int turn = 1; turn <= 5; turn++)
            {
                Console.WriteLine($"\n--- Turn {turn} ---");

                // Generate a random incident
                string incidentType = possibleIncidents[rng.Next(possibleIncidents.Length)];
                string location = cityLocations[rng.Next(cityLocations.Length)];
                Incident newIncident = new Incident(incidentType, location);

                Console.WriteLine($"Incident: {newIncident.Type} at {newIncident.Location}");

                bool incidentHandled = false;

                // Check which unit can respond
                foreach (var unit in responseUnits)
                {
                    if (unit.CanRespondTo(newIncident.Type))
                    {
                        unit.HandleIncident(newIncident);
                        playerScore += 10;
                        incidentHandled = true;
                        break;
                    }
                }

                if (!incidentHandled)
                {
                    Console.WriteLine("No unit available to handle this incident.");
                    playerScore -= 5;
                }

                Console.WriteLine($"Current Score: {playerScore}");
            }

            Console.WriteLine($"\n--- Simulation Complete ---");
            Console.WriteLine($"Final Score: {playerScore}");
        }
    }
}
