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
            initExpoAutoMapper();
            InitBllAutoMappeur.initialisation();
        }

        /// <summary>
        /// init mapper pour les objet de l'API et business
        /// </summary>
        private static void initExpoAutoMapper(){
            Mapper.CreateMap<EvenementUpdate, EvenementBll>();
            Mapper.CreateMap<EvenementBll, EvenementUpdate>();

            Mapper.CreateMap<EvenementFront, EvenementBll>();
            Mapper.CreateMap<EvenementBll, EvenementFront>();

            Mapper.CreateMap<EvenementCategorieBll, EvenementCategorieFront>();
            Mapper.CreateMap<EvenementCategorieFront, EvenementCategorieBll>();

            Mapper.CreateMap<EvenementBll, EvenementFront>();
            Mapper.CreateMap<EvenementFront, EvenementBll>();

            Mapper.CreateMap<EvenementBll, EvenementTimelineFront>();
            Mapper.CreateMap<EvenementTimelineFront, EvenementBll>();

            Mapper.CreateMap<EventStateBll, EventStateFront>();
            Mapper.CreateMap<EventStateFront, EventStateBll>();

            Mapper.CreateMap<EventLocationBll, EventLocationFront>();
            Mapper.CreateMap<EventLocationFront, EventLocationBll>();

            //création de map pour les string builder
            Mapper.CreateMap<string, StringBuilder>().ConvertUsing(s =>
            {
                StringBuilder sb = new StringBuilder(s);
                return sb;
            });
        }
    }
}