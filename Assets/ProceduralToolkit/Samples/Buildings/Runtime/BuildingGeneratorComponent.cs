using ProceduralToolkit.Buildings;
using ProceduralToolkit.Runtime.Buildings;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProceduralToolkit.Samples.Buildings.Runtime
{
    public class BuildingGeneratorComponent : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("facadePlanningStrategy")]
        private FacadePlanner facadePlanner;
        [SerializeField, FormerlySerializedAs("facadeConstructionStrategy")]
        private FacadeConstructor facadeConstructor;
        [SerializeField, FormerlySerializedAs("roofPlanningStrategy")]
        private RoofPlanner roofPlanner;
        [SerializeField, FormerlySerializedAs("roofConstructionStrategy")]
        private RoofConstructor roofConstructor;
        [SerializeField]
        private PolygonAsset foundationPolygon;
        [SerializeField]
        private BuildingGenerator.Config config = new();

        BuildingGenerator _generator;
        private void Awake()
        {
            SetUp();
        }

        public Transform GenerateBuilding()
        {
            return _generator.Generate(foundationPolygon.vertices, config);
        }
        
        void SetUp()
        {
            _generator = new BuildingGenerator();
            _generator.SetFacadePlanner(facadePlanner);
            _generator.SetFacadeConstructor(facadeConstructor);
            _generator.SetRoofPlanner(roofPlanner);
            _generator.SetRoofConstructor(roofConstructor);
        }
        
        public Transform Generate(Transform parent)
        {
            var generator = new BuildingGenerator();
            generator.SetFacadePlanner(facadePlanner);
            generator.SetFacadeConstructor(facadeConstructor);
            generator.SetRoofPlanner(roofPlanner);
            generator.SetRoofConstructor(roofConstructor);
            return generator.Generate(foundationPolygon.vertices, config, parent);
        }
        
        public Transform GenerateOld()
        {
            var generator = new BuildingGenerator();
            generator.SetFacadePlanner(facadePlanner);
            generator.SetFacadeConstructor(facadeConstructor);
            generator.SetRoofPlanner(roofPlanner);
            generator.SetRoofConstructor(roofConstructor);
            return generator.Generate(foundationPolygon.vertices, config);
        }

        public void SetFloors(int amount)
        {
            config.floors = amount;
        }

    }
}
