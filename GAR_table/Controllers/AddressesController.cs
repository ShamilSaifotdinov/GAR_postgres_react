using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using GAR_table.Models;
using GAR_table.DB;
using GAR_table.DB.Region;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GAR.Controllers
{
    public class AddressesController : ControllerBase
    {
        private readonly GarContext db;
        private readonly Dictionary<short, IGarContextRegion> regions;

        private readonly ILogger<AddressesController> _logger;

        #region xml

        // General
        static private readonly string path = "C:\\Practice\\Web\\GAR_table\\gar-xml\\";
        /*
        static private readonly XDocument xlevels = XDocument.Load(path + "AS_OBJECT_LEVELS_20230320_0c63ffea-e5ce-4b68-bd33-eca4ba0bb0e9.xml");
        static private readonly XElement? levels = xlevels.Element("OBJECTLEVELS");
        */
        static private readonly XDocument xparam_types = XDocument.Load(path + "AS_PARAM_TYPES_20230320_9337ae8a-c01e-41c7-a8d5-2e9a29f92fe5.xml");
        static private readonly XElement? obj_params_types = xparam_types.Element("PARAMTYPES");

        // Region

        static private readonly string pathWithRegion = path + "87\\";
        /*
        static private readonly XDocument xobjects = XDocument.Load(pathWithRegion + "AS_ADDR_OBJ_20230320_80f600ee-382e-49df-a495-7110e8e0950d.xml");
        static private readonly XElement? objects = xobjects.Element("ADDRESSOBJECTS");
        */
        static private readonly XDocument xparams = XDocument.Load(pathWithRegion + "AS_ADDR_OBJ_PARAMS_20230320_f1a5ba81-3292-4cec-8a60-20e89865601b.xml");
        static private readonly XElement? obj_params = xparams.Element("PARAMS");
        /*
        static private readonly XDocument xadm_hierarchy = XDocument.Load(pathWithRegion + "AS_ADM_HIERARCHY_20230320_f7bcb5da-0787-4d70-9c5d-e69b1011636e.xml");
        static private readonly XElement? adm_hierarchy = xadm_hierarchy.Element("ITEMS");

        static private readonly XDocument xmun_hierarchy = XDocument.Load(pathWithRegion + "AS_MUN_HIERARCHY_20230320_1741feec-eaee-41aa-8977-ed7937725753.xml");
        static private readonly XElement? mun_hierarchy = xmun_hierarchy.Element("ITEMS");
        */

        #endregion

        public AddressesController(ILogger<AddressesController> logger,
            GarContext context, 
            GarContextRegion50 context50,
            GarContextRegion66 context66,
            GarContextRegion86 context86, 
            GarContextRegion87 context87
            )
        {
            _logger = logger;
            db = context;
            regions = new Dictionary<short, IGarContextRegion>
            {
                [50] = context50,
                [66] = context66,
                [86] = context86,
                [87] = context87
            };
        }
        
        protected internal string? GetInfoTest(long? id, string type, IGarContextRegion region)
        {
            short? type_id = db.ParamTypes.FirstOrDefault(param => param.Code == type).Id;

            string? param_value = region.AddrObjParams.FirstOrDefault(param =>
                param.Objectid == id
                && param.Typeid == type_id
                && param.Changeidend == 0
                )?.Value;

            return param_value;
        }
        protected internal IEnumerable<GAR_elem_region> ItemInfoRegion(int? objId, short regionId, IGarContextRegion region)
        {
            var objects = region.AddrObjs.Where(addr_obj => 
                addr_obj.Objectid == objId
                && (addr_obj.Isactual ?? false)
                && (addr_obj.Isactive ?? false)
            ).ToList();

            List<GAR_elem_region> elems = new();

            if (objects is not null && objects.Any())
            {
                foreach (AddrObj obj in objects)
                {
                    long? ID = obj.Objectid;
                    short? LEVEL_ID = obj.Level;

                    var LEVEL_NAME = db.ObjectLevels.FirstOrDefault(level => level.Level == LEVEL_ID).Name;

                    elems.Add(new GAR_elem_region
                    {
                        regionId = regionId,
                        ID = obj.Objectid,
                        Name = obj.Name,
                        TypeName = obj.Typename,
                        Code = GetInfoTest(ID, "CODE", region),
                        OKATO = GetInfoTest(ID, "OKATO", region),
                        OKTMO = GetInfoTest(ID, "OKTMO", region),
                        Level = $"{LEVEL_ID}. {LEVEL_NAME}"
                    });
                }
            }
            return elems.ToArray();
        }
        protected internal IEnumerable<GAR_elem> ItemInfo(int? objId, IGarContextRegion region)
        {
            var objects = region.AddrObjs.Where(addr_obj =>
                addr_obj.Objectid == objId
                && (addr_obj.Isactual ?? false)
                && (addr_obj.Isactive ?? false)
            ).ToList();

            List<GAR_elem> elems = new();

            if (objects is not null && objects.Any())
            {
                foreach (AddrObj obj in objects)
                {
                    long? ID = obj.Objectid;
                    short? LEVEL_ID = obj.Level;

                    var LEVEL_NAME = db.ObjectLevels.FirstOrDefault(level => level.Level == LEVEL_ID).Name;

                    elems.Add(new GAR_elem
                    {
                        ID = obj.Objectid,
                        Name = obj.Name,
                        TypeName = obj.Typename,
                        Code = GetInfoTest(ID, "CODE", region),
                        OKATO = GetInfoTest(ID, "OKATO", region),
                        OKTMO = GetInfoTest(ID, "OKTMO", region),
                        Level = $"{LEVEL_ID}. {LEVEL_NAME}"
                    });
                }
            }
            return elems.ToArray();
        }

        //Routes

        [HttpGet]
        /* public IEnumerable<Param_info> Info(string id)
        {
            var xobj_params = obj_params?
                .Elements("PARAM")
                .Where(p => p.Attribute("OBJECTID")?.Value == id
                    && p.Attribute("CHANGEIDEND")?.Value == "0")
                .OrderBy(p => Int32.Parse(p.Attribute("TYPEID")?.Value));

            List<Param_info> obj_info = new();

            if (xobj_params != null)
            {
                foreach (XElement item in xobj_params)
                {
                    string? type_id = item.Attribute("TYPEID")?.Value;
                    obj_info.Add(new Param_info
                    {
                        ID = type_id,
                        Name = obj_params_types?
                            .Elements("PARAMTYPE")
                            .FirstOrDefault(p => p.Attribute("ID")?.Value == type_id)?
                            .Attribute("NAME")?
                            .Value,
                        Value = item.Attribute("VALUE")?.Value
                    });
                }
            }
            return obj_info;
        }
        public IEnumerable<Param_info> InfoTest(short region, int id)
        */
        public Info InfoTest(short region, int id)
        {
            IGarContextRegion regionObj = regions[region];

            var objParams = regionObj.AddrObjParams.Where(param =>
                param.Objectid == id
                && param.Changeidend == 0)
                .OrderBy(param => param.Typeid);


            List<Param_info> obj_info = new();

            if (objParams != null)
            {
                foreach (var item in objParams)
                {
                    short type_id = item.Typeid;
                    obj_info.Add(new Param_info
                    {
                        ID = type_id,
                        Name = db.ParamTypes.FirstOrDefault(p => p.Id == type_id)?.Name,
                        Value = item.Value
                    });
                }
            }
            
            var geoms = db.RegionsRves.FromSqlRaw($"SELECT ST_AsGeoJSON(public.regions_rf.*) geom FROM public.regions_rf WHERE public.regions_rf.cladr_code = '{GetInfoTest(id, "CODE", regionObj)}';").ToList();
            
            return new Info { Param_info = obj_info, geom = geoms.Count > 0 ? geoms[0].geom : null };
        }
        public IEnumerable<GAR_elem_region> RegionsTest()
        {
            List<GAR_elem_region> addr_objs = new();

            foreach (var elem in regions)
            {
                IGarContextRegion regionObj = elem.Value;
                IEnumerable<AdmHierarchy>? Hierarchy = regionObj.AdmHierarchies.Where(Hierarchy => 
                    Hierarchy.Parentobjid == 0
                    && (Hierarchy.Isactive ?? false)
                ).ToList();

                foreach (AdmHierarchy obj in Hierarchy)
                {
                    var addrObj = ItemInfoRegion(obj.Objectid, elem.Key, regionObj);

                    if (addrObj != null)
                    {
                        addr_objs = addr_objs.Concat(addrObj).ToList();
                    }
                }
            }

            return addr_objs;
        }
        public IEnumerable<GAR_elem> admTest(short region, int id)
        {
            IGarContextRegion regionObj = regions[region];

            IEnumerable<AdmHierarchy>? Hierarchy = regionObj.AdmHierarchies.Where(Hierarchy =>
                Hierarchy.Parentobjid == id
                && (Hierarchy.Isactive ?? false)).ToList();

            List<GAR_elem> addr_objs = new();

            foreach (AdmHierarchy obj in Hierarchy)
            {
                var addrObj = ItemInfo(obj.Objectid, regionObj);

                if (addrObj != null)
                {
                    addr_objs = addr_objs.Concat(addrObj).ToList();
                }
            }

            return addr_objs;
        }
        /* public IEnumerable<GAR_elem> admTest2(short region, int id)
        public IEnumerable<AdmHierarchy> admTest2(short region, int id)
        {
            IGarContextRegion regionObj = regions[region];

            //var addr_objs = from AdmHierarchy in regionObj.AdmHierarchies
            //                join AddrObj in regionObj.AddrObjs on AdmHierarchy.Objectid equals AddrObj.Objectid
            //                join Level in db.ObjectLevels on AddrObj.Level equals Level.Level
            //                join AddrObjParam in regionObj.AddrObjParams on (long?)AddrObj.Objectid equals AddrObjParam.Objectid
            //                join ParamType in db.ParamTypes on AddrObjParam.Typeid equals ParamType.Id
            //                where AdmHierarchy.Parentobjid == id
            //                select new GAR_elem
            //                {
            //                    ID = AddrObj.Objectid,
            //                    Name = AddrObj.Name,
            //                    TypeName = AddrObj.Typename,
            //                    //Code = GetInfoTest(ID, "CODE", region),
            //                    //OKATO = GetInfoTest(ID, "OKATO", region),
            //                    //OKTMO = GetInfoTest(ID, "OKTMO", region),
            //                    Level = $"{AddrObj.Level}. {Level.Name}"
            //                };
            var addr_objs = db.Database.FromSql($"SELECT * FROM _{region}.\"ADM_HIERARCHY\" WHERE \"PARENTOBJID\" = {id}").ToList();

            return addr_objs;
        }
        */
        public IEnumerable<GAR_elem> munTest(short region, int id)
        {
            IGarContextRegion regionObj = regions[region];

            IEnumerable<MunHierarchy>? Hierarchy = regionObj.MunHierarchies.Where(Hierarchy =>
                Hierarchy.Parentobjid == id
                && (Hierarchy.Isactive ?? false)).ToList();

            List<GAR_elem> addr_objs = new();

            foreach (MunHierarchy obj in Hierarchy)
            {
                var addrObj = ItemInfo(obj.Objectid, regionObj);

                if (addrObj != null)
                {
                    addr_objs = addr_objs.Concat(addrObj).ToList();
                }
            }

            return addr_objs;
        }
    }
}