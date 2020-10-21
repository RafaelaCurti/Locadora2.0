using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;

namespace Locadora.Services
{
    public partial class TItenService : EntityService<TIten>, ITItenService
    {
        public void SaveIten(TReservation model)
        {
            TIten.Delete(x => x.Movie.Id == model.Id);
            if (model.Itens != null)
            {
                for (int i = 0; i < model.Itens.Length; i++)
                {
                    if (model.Itens[i] != 0)
                    {
                        new TIten()
                        {
                            Reservation = model,
                            Movie = TMovie.Load(model.Itens[i])
                        }.Save();
                    }
                }
            }
        }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Entities;
//using Locadora.Domain;

//namespace Locadora.Services
//{
//    public partial class TItenService : EntityService<TIten>, ITItenService
//    {
//        public void SaveIten(TReservation model)
//        {
//            TIten.Delete(x => x.Movie.Id == model.Id);
//            if (model.Iten != null)
//            for (int i = 0; i < model.Iten.Length; i++)
//            {
//                new TIten()
//                {
//                    Reservation = model,
//                    Movie = TMovie.Load(model.Iten[i])
//                }.Save();
//            }
//        }

//        //{
//        //    TIten.Delete(x => x.Movie.Id == model.Id);
//        //    if (model.Iten != null)
//        //    {
//        //        for (int i = 0; i < model.Iten.Length; i++)
//        //        {
//        //            new TIten()
//        //            {
//        //                Reservation = model,
//        //                Movie = TMovie.Load(model.Iten[i])
//        //            }.Save();
//        //        }
//        //    }
//        //}
//    }
//}
