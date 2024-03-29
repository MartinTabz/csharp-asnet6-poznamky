﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using PraktickaMaturita.Data;
using PraktickaMaturita.Models;
using System.Diagnostics;

namespace PraktickaMaturita.Controllers
{
    public class PoznamkyController : Controller
    {
        private ArchivPoznamekData _databaze;

        public PoznamkyController(ArchivPoznamekData databaze)
        {
            _databaze = databaze;
        }

        [HttpGet]
        public IActionResult Pridat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Pridat(string nadpis, string popis, string dulezity)
        {
            if (nadpis == null || nadpis.Trim().Length == 0)
                return RedirectToAction("Pridat");

            Uzivatel? prihlasenyUzivatel = KdoJePrihlasen();

            Poznamka pridavanaPoznamka = new Poznamka()
            {
                Dulezitost = (dulezity == "ano"),
                DatumVlozeni = DateTime.Now,
                Nadpis = nadpis,
                Popis = popis,
                Autor = prihlasenyUzivatel,
            };

            _databaze.Add(pridavanaPoznamka);
            _databaze.SaveChanges();

            return RedirectToAction("Vypsat", "Poznamky");
        }

        [HttpGet]
        public IActionResult Vypsat()
        {
            Uzivatel? prihlasenyUzivatel = KdoJePrihlasen();

            List<Poznamka> vsechnyUkoly = _databaze.Poznamky
                .Where(u => u.Autor == prihlasenyUzivatel)
                .ToList();

            return View(vsechnyUkoly);
        }

        public IActionResult Poznamka(int id)
        {
            Debug.WriteLine("id:" + id);
            Poznamka vsechnyPoznamky = _databaze.Poznamky
                .Where(u => u.Id == id)
                .FirstOrDefault();


            ViewData["name"] = vsechnyPoznamky.Nadpis;

            return View(vsechnyPoznamky);
        }

        private Uzivatel? KdoJePrihlasen()
        {
            string? jmenoPrihlasenehoUzivatele = HttpContext.Session.GetString("Prihlaseny");

            if (jmenoPrihlasenehoUzivatele == null)
                return null;

            Uzivatel? prihlasenyUzivatel
                = _databaze.Uzivatele
                .Where(u => u.Jmeno == jmenoPrihlasenehoUzivatele)
                .FirstOrDefault();

            return prihlasenyUzivatel;
        }

        [HttpGet]
        public IActionResult Smazat(int id)
        {
            Uzivatel? prihlasenyUzivatel = KdoJePrihlasen();

            if (prihlasenyUzivatel == null)
                return RedirectToAction("Prihlasit", "Uzivatel");

            Poznamka? mazanaPoznamka = _databaze.Poznamky
                .Where(u => u.Id == id)
                .FirstOrDefault();

            if (mazanaPoznamka != null && mazanaPoznamka.Autor == prihlasenyUzivatel)
            {
                _databaze.Poznamky.Remove(mazanaPoznamka);
                _databaze.SaveChanges();
            }

            return RedirectToAction("Vypsat");
        }
    }
}
