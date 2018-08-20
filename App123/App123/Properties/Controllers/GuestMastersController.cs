using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure; 
using System.Linq;
using System.Net;
using System.Net.Http; 
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;

using WebApplication2.Algoritham;
using Newtonsoft.Json;
using System.IO;

namespace WebApplication2.Controllers
{
    public class GuestMastersController : ApiController
    {
        private CrudContext db = new CrudContext(); 

        // GET: api/GuestMasters
        public IQueryable<GuestMaster> GetGuestMaster()
        {
            return db.GuestMaster;
        } 
        // GET: api/GuestMasters/5
        [ResponseType(typeof(GuestMaster))]
        public DataTableResponse GetGuestMaster(int id)
        {
            var guestlist = db.GuestMaster.Where(cs => cs.EventId == id);
            return new DataTableResponse
            {
                recordsTotal = guestlist.Count(),
                recordsFiltered = 10,
                data = guestlist.ToArray()
            };
        }

        // PUT: api/GuestMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuestMaster(int id, GuestMaster guestMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guestMaster.GuestId)
            {
                return BadRequest();
            }

            db.Entry(guestMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/GuestMasters
        [ResponseType(typeof(GuestMaster))]
        public IHttpActionResult PostGuestMaster(GuestMaster guestMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GuestMaster.Add(guestMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = guestMaster.GuestId }, guestMaster);
        }

        // DELETE: api/GuestMasters/5
        [ResponseType(typeof(GuestMaster))]
        public IHttpActionResult DeleteGuestMaster(int id)
        {
            GuestMaster guestMaster = db.GuestMaster.Find(id);
            if (guestMaster == null)
            {
                return NotFound();
            }

            db.GuestMaster.Remove(guestMaster);
            db.SaveChanges();

            return Ok(guestMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestMasterExists(int id)
        {
            return db.GuestMaster.Count(e => e.GuestId == id) > 0;
        }

        private void InitializePopulation(string inputFile, string populationSize)
        {
            var jsSerializer = new JsonSerializer();
            var textReader = new StreamReader(inputFile);
            var reader = new JsonTextReader(textReader);
            var cfg = jsSerializer.Deserialize<Configuration>(reader);
            textReader.Close();
            reader.Close();

            Runner = new Runner(cfg, int.Parse(populationSize));

            Console.WriteLine("Initialized");
            Console.WriteLine("Best table score is: " + Runner.BestScore());
        }

        private void RunGenerations(string number)
        {
            for (var i = 0; i < int.Parse(number); i++)
            {
                Runner.RunGeneration();
                Console.WriteLine("Generation " + (i + 1) + ": " + Runner.BestScore() + " (avg: " + Runner.AverageScore() + ")");
            }
        }

        private void SaveBest(string output)
        {
            var serializer = new JsonSerializer();

            var textWriter = new StreamWriter(output);
            var writer = new JsonTextWriter(textWriter) { Formatting = Formatting.Indented, Indentation = 4 };
            serializer.Serialize(writer, Runner.CurrentArrangement);
            textWriter.Flush();
            textWriter.Close();

            textWriter = new StreamWriter("pretty-" + output);
            foreach (var table in Runner.CurrentArrangement.Tables)
            {
                textWriter.WriteLine("Table (" + table.Score + ")");
                textWriter.WriteLine("".PadRight(80, '='));

                foreach (var person in table.People.Where(x => x != null))
                    textWriter.WriteLine(person.Name);

                textWriter.WriteLine("\n");
            }
            textWriter.Flush();
            textWriter.Close();
        }

        private void ParseNames(string input, string output)
        {
            var stringReader = new StreamReader(input);
            var data = stringReader.ReadToEnd();
            stringReader.Close();
            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var cfg = new Configuration();
            foreach (var line in lines)
            {
                var currentLine = line;
                if (cfg.People.Count(x => x.Name == currentLine) > 0)
                    throw new ApplicationException("Name duplication: " + line);

                cfg.People.Add(new Person
                {
                    Name = line
                });
            }

            var serializer = new JsonSerializer();
            var textWriter = new StreamWriter(output);
            var writer = new JsonTextWriter(textWriter) { Formatting = Formatting.Indented, Indentation = 4 };
            serializer.Serialize(writer, cfg);
            textWriter.Flush();
            textWriter.Close();

            Console.WriteLine("Wrote out " + lines.Count() + " guests");
            Console.WriteLine();
        }

        private void ParseRelations(string input, string output)
        {
            var serializer = new JsonSerializer();
            var textReader = new StreamReader(output);
            var reader = new JsonTextReader(textReader);
            var cfg = serializer.Deserialize<Configuration>(reader);
            textReader.Close();
            reader.Close();

            var stringReader = new StreamReader(input);
            var data = stringReader.ReadToEnd();
            stringReader.Close();
            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var currentLine = line;
                var left = currentLine.Split(new[] { " + " }, StringSplitOptions.RemoveEmptyEntries)[0];
                var right = currentLine.Split(new[] { " + " }, StringSplitOptions.RemoveEmptyEntries)[1];

                if (cfg.People.Count(x => x.Name == left) == 0)
                    throw new ApplicationException("Name not found: " + left);
                if (cfg.People.Count(x => x.Name == right) == 0)
                    throw new ApplicationException("Name not found: " + right);

                cfg.Relationships.Add(new Relationship()
                {
                    Left = left,
                    Right = right,
                    Score = 100
                });
            }


            var textWriter = new StreamWriter(output);
            var writer = new JsonTextWriter(textWriter) { Formatting = Formatting.Indented, Indentation = 4 };
            serializer.Serialize(writer, cfg);
            textWriter.Flush();
            textWriter.Close();

            Console.WriteLine("Wrote out " + lines.Count() + " relationships");
            Console.WriteLine();
        }

        private void DisplayHelp()
        {
            foreach (var cmd in Commands.OrderBy(x => x.Key))
            {
                Console.WriteLine(cmd.Value.Help);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private Dictionary<string, CommandRunner> Commands { get; set; }
        private Runner Runner { get; set; }
    }
}