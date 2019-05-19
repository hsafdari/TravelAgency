using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class SelectedFlight : BaseEntity
    {
        #region Properties
        public string AirlineIATACode { get; set; }
        public string FlightNo { get; set; }
        public DateTime FlightDateTime { get; set; }
        public string FromIATACode { get; set; }
        public string ToIATACode { get; set; }
        public FlightType FlightType { get; set; }
        public EnumFlightDirectionType FlightDirection { get; set; }
        /// <summary>
        /// میزان بار مجاز
        /// </summary>
        public string BaggageAmount { get; set; }
        #endregion

        #region Reference navigation properties
        public int TourScheduleId { get; set; }
        public virtual TourSchedule TourSchedule { get; set; }

        public long OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Nullable<int> CompanyTransferId { get; set; }
        public virtual CompanyTransfer CompanyTransfer { get; set; }
        public Nullable<int> VehicleTypeClassId { get; set; }
        public virtual VehicleTypeClass VehicleTypeClass { get; set; }
        #endregion
    }
}
