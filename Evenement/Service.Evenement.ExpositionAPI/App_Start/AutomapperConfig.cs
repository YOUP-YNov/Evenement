﻿using AutoMapper;
using Service.Evenement.Business;
using Service.Evenement.ExpositionAPI.Controllers;
using Service.Evenement.ExpositionAPI.Models;
using Service.Evenement.ExpositionAPI.Models.ModelsUpdate;
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

            Mapper.CreateMap<EvenementBll, EvenementTimelineFront>().ForMember(e => e.Evenement_id, d => d.MapFrom(src => src.Id)).ForMember(e => e.LieuEvenement_id, d => d.MapFrom(src => src.EventAdresse.Id)).ForMember(e => e.Prix, d => d.MapFrom(src => src.Price)).ForMember(e => e.Etat_id, d => d.MapFrom(src => src.EtatEvenement.Id));
            Mapper.CreateMap<EvenementTimelineFront, EvenementBll>().ForMember(e => e.Id, d => d.MapFrom(src => src.Evenement_id)).ForMember(e => e.EventAdresse, d => d.MapFrom(src => src)).ForMember(e => e.Price, d => d.MapFrom(src => src.Prix))/*.ForMember(e => e.EtatEvenement.Id, d => d.MapFrom(src => src.Etat_id))*/;

            Mapper.CreateMap<EventStateBll, EventStateFront>();
            Mapper.CreateMap<EventStateFront, EventStateBll>();

            Mapper.CreateMap<EventLocationBll, EventLocationFront>();
            Mapper.CreateMap<EventLocationFront, EventLocationBll>();

            Mapper.CreateMap<EvenementCategorieBll, EvenementCategorieFront>();
            Mapper.CreateMap<EvenementCategorieFront,EvenementCategorieBll>();

            Mapper.CreateMap<CategorieUpdate, EvenementCategorieFront>();
            Mapper.CreateMap<EvenementCategorieFront, CategorieUpdate>();

            Mapper.CreateMap<CategorieUpdate, EvenementCategorieBll>();
            Mapper.CreateMap<EvenementCategorieBll, CategorieUpdate>();

            Mapper.CreateMap<EventImageFront, EventImageBll>();
            Mapper.CreateMap<EventImageBll, EventImageFront>();
            //création de map pour les string builder
            Mapper.CreateMap<string, StringBuilder>().ConvertUsing(s =>
            {
                StringBuilder sb = new StringBuilder(s);
                return sb;
            });


           
        }
    }
}