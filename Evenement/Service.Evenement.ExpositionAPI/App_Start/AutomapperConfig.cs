using AutoMapper;
using Service.Evenement.Business;
using Service.Evenement.ExpositionAPI.Controllers;
using Service.Evenement.ExpositionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Service.Evenement.ExpositionAPI.App_Start
{
    /// <summary>
    /// Classe d'initialisation de l'automapper
    /// </summary>
    public class AutomapperConfig
    {
        /// <summary>
        /// Initialisation de l'autoMapper.
        /// Une fois l'initialisation faite, tous les objets objets peuvent être mapper
        /// </summary>
        public static void initAutoMapper(){
            Mapper.CreateMap<EvenementUpdate, EvenementBll>();
            Mapper.CreateMap<EvenementFront, EvenementBll>();
            Mapper.CreateMap<EvenementBll, EvenementTimelineFront>();

            //création de map pour les string builder
            Mapper.CreateMap<string, StringBuilder>().ConvertUsing(s =>
            {
                StringBuilder sb = new StringBuilder(s);
                return sb;
            });

            //mapping particulier de l'eventFront vers event Location
            Mapper.CreateMap<EventLocationFront, EventLocationBll>().ConvertUsing(loc =>
            {
                EventLocationBll location = new EventLocationBll();
                location.Adresse = new StringBuilder(loc.Adresse);
                location.Pays = new StringBuilder(loc.Pays);
                location.Ville = new StringBuilder(loc.Ville);
                location.Latitude = loc.Latitude;
                location.Longitude = loc.Longitude;
                location.CodePostale = new StringBuilder(loc.CodePostale);
                return location;
            });
        }
    }
}