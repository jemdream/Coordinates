using System;
using System.Runtime.CompilerServices;
using Etg.SimpleStubs;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;
using System.Collections.Generic;
using Coordinates.Measurements.Types;
using System.Reactive.Disposables;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Elements
{
    [CompilerGenerated]
    public class StubIElement : IElement
    {
        private readonly StubContainer<StubIElement> _stubs = new StubContainer<StubIElement>();

        int global::Coordinates.Measurements.Elements.IElement.RequiredMeasurementCount
        {
            get
            {
                return _stubs.GetMethodStub<RequiredMeasurementCount_Get_Delegate>("get_RequiredMeasurementCount").Invoke();
            }
        }

        global::Coordinates.Measurements.Elements.PlaneEnum? global::Coordinates.Measurements.Elements.IElement.Plane
        {
            get
            {
                return _stubs.GetMethodStub<Plane_Get_Delegate>("get_Plane").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<Plane_Set_Delegate>("set_Plane").Invoke(value);
            }
        }

        global::Coordinates.Models.DTO.Position global::Coordinates.Measurements.Elements.IElement.InitialPosition
        {
            get
            {
                return _stubs.GetMethodStub<InitialPosition_Get_Delegate>("get_InitialPosition").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<InitialPosition_Set_Delegate>("set_InitialPosition").Invoke(value);
            }
        }

        global::Coordinates.Measurements.Helpers.ObservableList<global::Coordinates.Models.DTO.Position> global::Coordinates.Measurements.Elements.IElement.SelectedPositions
        {
            get
            {
                return _stubs.GetMethodStub<SelectedPositions_Get_Delegate>("get_SelectedPositions").Invoke();
            }
        }

        global::Coordinates.Measurements.Helpers.ObservableList<global::Coordinates.Models.DTO.Position> global::Coordinates.Measurements.Elements.IElement.Positions
        {
            get
            {
                return _stubs.GetMethodStub<Positions_Get_Delegate>("get_Positions").Invoke();
            }
        }

        public delegate int RequiredMeasurementCount_Get_Delegate();

        public StubIElement RequiredMeasurementCount_Get(RequiredMeasurementCount_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.Measurements.Elements.PlaneEnum? Plane_Get_Delegate();

        public StubIElement Plane_Get(Plane_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void Plane_Set_Delegate(global::Coordinates.Measurements.Elements.PlaneEnum? value);

        public StubIElement Plane_Set(Plane_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.Models.DTO.Position InitialPosition_Get_Delegate();

        public StubIElement InitialPosition_Get(InitialPosition_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void InitialPosition_Set_Delegate(global::Coordinates.Models.DTO.Position value);

        public StubIElement InitialPosition_Set(InitialPosition_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.Elements.IElement.AxisMovementValidation(global::Coordinates.Models.DTO.Position incomingPosition)
        {
            return _stubs.GetMethodStub<AxisMovementValidation_Position_Delegate>("AxisMovementValidation").Invoke(incomingPosition);
        }

        public delegate bool AxisMovementValidation_Position_Delegate(global::Coordinates.Models.DTO.Position incomingPosition);

        public StubIElement AxisMovementValidation(AxisMovementValidation_Position_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.Elements.IElement.CanCalculate()
        {
            return _stubs.GetMethodStub<CanCalculate_Delegate>("CanCalculate").Invoke();
        }

        public delegate bool CanCalculate_Delegate();

        public StubIElement CanCalculate(CanCalculate_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::Coordinates.Measurements.Models.ICalculationResult global::Coordinates.Measurements.Elements.IElement.Calculate()
        {
            return _stubs.GetMethodStub<Calculate_Delegate>("Calculate").Invoke();
        }

        public delegate global::Coordinates.Measurements.Models.ICalculationResult Calculate_Delegate();

        public StubIElement Calculate(Calculate_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.Measurements.Helpers.ObservableList<global::Coordinates.Models.DTO.Position> SelectedPositions_Get_Delegate();

        public StubIElement SelectedPositions_Get(SelectedPositions_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.Measurements.Helpers.ObservableList<global::Coordinates.Models.DTO.Position> Positions_Get_Delegate();

        public StubIElement Positions_Get(Positions_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace Coordinates.Measurements.Export
{
    [CompilerGenerated]
    public class StubIMeasurementsExporter : IMeasurementsExporter
    {
        private readonly StubContainer<StubIMeasurementsExporter> _stubs = new StubContainer<StubIMeasurementsExporter>();

        global::System.Collections.Generic.IEnumerable<global::Coordinates.Measurements.Export.MeasurementExportFormat> global::Coordinates.Measurements.Export.IMeasurementsExporter.Formats
        {
            get
            {
                return _stubs.GetMethodStub<Formats_Get_Delegate>("get_Formats").Invoke();
            }
        }

        public delegate global::System.Collections.Generic.IEnumerable<global::Coordinates.Measurements.Export.MeasurementExportFormat> Formats_Get_Delegate();

        public StubIMeasurementsExporter Formats_Get(Formats_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace Coordinates.Measurements
{
    [CompilerGenerated]
    public class StubIMeasurementManager : IMeasurementManager
    {
        private readonly StubContainer<StubIMeasurementManager> _stubs = new StubContainer<StubIMeasurementManager>();

        global::System.Collections.Generic.IEnumerable<global::Coordinates.Measurements.Types.MeasurementMethodEnum> global::Coordinates.Measurements.IMeasurementManager.AvailableMeasurementMethods
        {
            get
            {
                return _stubs.GetMethodStub<AvailableMeasurementMethods_Get_Delegate>("get_AvailableMeasurementMethods").Invoke();
            }
        }

        global::Coordinates.Measurements.Types.MeasurementMethodEnum? global::Coordinates.Measurements.IMeasurementManager.SelectedMeasurementMethod
        {
            get
            {
                return _stubs.GetMethodStub<SelectedMeasurementMethod_Get_Delegate>("get_SelectedMeasurementMethod").Invoke();
            }
        }

        global::System.IObservable<global::Coordinates.Measurements.Types.IMeasurementMethod> global::Coordinates.Measurements.IMeasurementManager.MeasurementSource
        {
            get
            {
                return _stubs.GetMethodStub<MeasurementSource_Get_Delegate>("get_MeasurementSource").Invoke();
            }
        }

        bool global::Coordinates.Measurements.IMeasurementManager.GatherData
        {
            get
            {
                return _stubs.GetMethodStub<GatherData_Get_Delegate>("get_GatherData").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<GatherData_Set_Delegate>("set_GatherData").Invoke(value);
            }
        }

        global::System.IObservable<global::Coordinates.Models.DTO.Position> global::Coordinates.Measurements.IMeasurementManager.PositionSource
        {
            get
            {
                return _stubs.GetMethodStub<PositionSource_Get_Delegate>("get_PositionSource").Invoke();
            }
        }

        global::Coordinates.Measurements.Helpers.ObservableList<global::Coordinates.Models.DTO.Position> global::Coordinates.Measurements.IMeasurementManager.PositionBuffer
        {
            get
            {
                return _stubs.GetMethodStub<PositionBuffer_Get_Delegate>("get_PositionBuffer").Invoke();
            }
        }

        public delegate global::System.Collections.Generic.IEnumerable<global::Coordinates.Measurements.Types.MeasurementMethodEnum> AvailableMeasurementMethods_Get_Delegate();

        public StubIMeasurementManager AvailableMeasurementMethods_Get(AvailableMeasurementMethods_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.Measurements.Types.MeasurementMethodEnum? SelectedMeasurementMethod_Get_Delegate();

        public StubIMeasurementManager SelectedMeasurementMethod_Get(SelectedMeasurementMethod_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::System.IObservable<global::Coordinates.Measurements.Types.IMeasurementMethod> MeasurementSource_Get_Delegate();

        public StubIMeasurementManager MeasurementSource_Get(MeasurementSource_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate bool GatherData_Get_Delegate();

        public StubIMeasurementManager GatherData_Get(GatherData_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void GatherData_Set_Delegate(bool value);

        public StubIMeasurementManager GatherData_Set(GatherData_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.IMeasurementManager.SetupMeasurementMethod(global::Coordinates.Measurements.Types.MeasurementMethodEnum selectedMeasurementMethod)
        {
            return _stubs.GetMethodStub<SetupMeasurementMethod_MeasurementMethodEnum_Delegate>("SetupMeasurementMethod").Invoke(selectedMeasurementMethod);
        }

        public delegate bool SetupMeasurementMethod_MeasurementMethodEnum_Delegate(global::Coordinates.Measurements.Types.MeasurementMethodEnum selectedMeasurementMethod);

        public StubIMeasurementManager SetupMeasurementMethod(SetupMeasurementMethod_MeasurementMethodEnum_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.IMeasurementManager.ResetMeasurementData()
        {
            return _stubs.GetMethodStub<ResetMeasurementData_Delegate>("ResetMeasurementData").Invoke();
        }

        public delegate bool ResetMeasurementData_Delegate();

        public StubIMeasurementManager ResetMeasurementData(ResetMeasurementData_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::System.IObservable<global::Coordinates.Models.DTO.Position> PositionSource_Get_Delegate();

        public StubIMeasurementManager PositionSource_Get(PositionSource_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.Measurements.Helpers.ObservableList<global::Coordinates.Models.DTO.Position> PositionBuffer_Get_Delegate();

        public StubIMeasurementManager PositionBuffer_Get(PositionBuffer_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.IMeasurementManager.Calibrate()
        {
            return _stubs.GetMethodStub<Calibrate_Delegate>("Calibrate").Invoke();
        }

        public delegate bool Calibrate_Delegate();

        public StubIMeasurementManager Calibrate(Calibrate_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace Coordinates.Measurements.Models
{
    [CompilerGenerated]
    public class StubICalculationResult : ICalculationResult
    {
        private readonly StubContainer<StubICalculationResult> _stubs = new StubContainer<StubICalculationResult>();
    }
}

namespace Coordinates.Measurements.Types
{
    [CompilerGenerated]
    public class StubIMeasurementMethod : IMeasurementMethod
    {
        private readonly StubContainer<StubIMeasurementMethod> _stubs = new StubContainer<StubIMeasurementMethod>();

        global::System.Collections.Generic.IEnumerable<global::Coordinates.Measurements.Elements.IElement> global::Coordinates.Measurements.Types.IMeasurementMethod.Elements
        {
            get
            {
                return _stubs.GetMethodStub<Elements_Get_Delegate>("get_Elements").Invoke();
            }
        }

        global::Coordinates.Measurements.Elements.IElement global::Coordinates.Measurements.Types.IMeasurementMethod.ActiveElement
        {
            get
            {
                return _stubs.GetMethodStub<ActiveElement_Get_Delegate>("get_ActiveElement").Invoke();
            }
        }

        bool global::Coordinates.Measurements.Types.IMeasurementMethod.IsNextElementAvailable
        {
            get
            {
                return _stubs.GetMethodStub<IsNextElementAvailable_Get_Delegate>("get_IsNextElementAvailable").Invoke();
            }
        }

        global::System.Reactive.Disposables.CompositeDisposable global::Coordinates.Measurements.Types.IMeasurementMethod.Subscriptions
        {
            get
            {
                return _stubs.GetMethodStub<Subscriptions_Get_Delegate>("get_Subscriptions").Invoke();
            }
        }

        public delegate global::System.Collections.Generic.IEnumerable<global::Coordinates.Measurements.Elements.IElement> Elements_Get_Delegate();

        public StubIMeasurementMethod Elements_Get(Elements_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.Measurements.Elements.IElement ActiveElement_Get_Delegate();

        public StubIMeasurementMethod ActiveElement_Get(ActiveElement_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::Coordinates.Measurements.Elements.IElement global::Coordinates.Measurements.Types.IMeasurementMethod.ActivateNextElement()
        {
            return _stubs.GetMethodStub<ActivateNextElement_Delegate>("ActivateNextElement").Invoke();
        }

        public delegate global::Coordinates.Measurements.Elements.IElement ActivateNextElement_Delegate();

        public StubIMeasurementMethod ActivateNextElement(ActivateNextElement_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.Types.IMeasurementMethod.SetupPlane(global::Coordinates.Measurements.Elements.PlaneEnum? plane)
        {
            return _stubs.GetMethodStub<SetupPlane_PlaneEnum_Delegate>("SetupPlane").Invoke(plane);
        }

        public delegate bool SetupPlane_PlaneEnum_Delegate(global::Coordinates.Measurements.Elements.PlaneEnum? plane);

        public StubIMeasurementMethod SetupPlane(SetupPlane_PlaneEnum_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.Types.IMeasurementMethod.SetupInitialPosition(global::Coordinates.Models.DTO.Position position)
        {
            return _stubs.GetMethodStub<SetupInitialPosition_Position_Delegate>("SetupInitialPosition").Invoke(position);
        }

        public delegate bool SetupInitialPosition_Position_Delegate(global::Coordinates.Models.DTO.Position position);

        public StubIMeasurementMethod SetupInitialPosition(SetupInitialPosition_Position_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate bool IsNextElementAvailable_Get_Delegate();

        public StubIMeasurementMethod IsNextElementAvailable_Get(IsNextElementAvailable_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        bool global::Coordinates.Measurements.Types.IMeasurementMethod.CanCalculate()
        {
            return _stubs.GetMethodStub<CanCalculate_Delegate>("CanCalculate").Invoke();
        }

        public delegate bool CanCalculate_Delegate();

        public StubIMeasurementMethod CanCalculate(CanCalculate_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::Coordinates.Measurements.Models.ICalculationResult global::Coordinates.Measurements.Types.IMeasurementMethod.Calculate()
        {
            return _stubs.GetMethodStub<Calculate_Delegate>("Calculate").Invoke();
        }

        public delegate global::Coordinates.Measurements.Models.ICalculationResult Calculate_Delegate();

        public StubIMeasurementMethod Calculate(Calculate_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::System.Reactive.Disposables.CompositeDisposable Subscriptions_Get_Delegate();

        public StubIMeasurementMethod Subscriptions_Get(Subscriptions_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}