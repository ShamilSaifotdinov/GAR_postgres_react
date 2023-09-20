-- DROP FUNCTION gar_elem_func(region_code integer, elem_id int, type text);

-- CREATE OR REPLACE FUNCTION GAR_elem_func(region_code int, elem_id int, type text)
-- RETURNS TABLE (
-- 	"OBJECTID" int, 
-- 	"NAME" character varying, 
-- 	"TYPENAME" character varying, 
-- 	Code text, 
-- 	OKATO text,
-- 	OKTMO text,
-- 	Level text) 
-- AS $$
-- BEGIN
-- 	RETURN QUERY EXECUTE '
-- 	SELECT 
-- 		 _' || region_code || '."ADDR_OBJ"."OBJECTID",
-- 		 _' || region_code || '."ADDR_OBJ"."NAME",
-- 		 _' || region_code || '."ADDR_OBJ"."TYPENAME",
-- 		(SELECT "VALUE" FROM _' || region_code || '."ADDR_OBJ_PARAMS"
-- 		 	WHERE "OBJECTID" = '|| elem_id ||' AND "TYPEID" = 10 LIMIT 1) AS Code,
-- 		(SELECT "VALUE" FROM _' || region_code || '."ADDR_OBJ_PARAMS"
-- 			WHERE "OBJECTID" = '|| elem_id ||' AND "TYPEID" = 6 LIMIT 1) AS OKATO,
-- 		(SELECT "VALUE" FROM _' || region_code || '."ADDR_OBJ_PARAMS"
-- 			WHERE "OBJECTID" = '|| elem_id ||' AND "TYPEID" = 7 LIMIT 1) AS OKTMO,
-- 		 _' || region_code || '."ADDR_OBJ"."LEVEL" || ''. '' || public."OBJECT_LEVELS"."NAME"
	
-- 	FROM _' || region_code || '."'|| type || '_HIERARCHY"
	
-- 	JOIN _' || region_code || '."ADDR_OBJ"
-- 	ON _' || region_code || '."'|| type || '_HIERARCHY"."OBJECTID" = _' || region_code || '."ADDR_OBJ"."OBJECTID"
	
-- 	JOIN public."OBJECT_LEVELS"
-- 	ON _' || region_code || '."ADDR_OBJ"."LEVEL" = public."OBJECT_LEVELS"."LEVEL"
	
-- 	WHERE "PARENTOBJID" = ' || elem_id || '
-- 	AND _' || region_code || '."'|| type || '_HIERARCHY"."ISACTIVE" = true
-- 	AND _' || region_code || '."ADDR_OBJ"."ISACTUAL" = true
-- 	AND _' || region_code || '."ADDR_OBJ"."ISACTIVE" = true
-- 	;';
-- END;
-- $$ LANGUAGE plpgsql;

SELECT * FROM gar_elem_func(86, 1453714, 'ADM')
Where Level = '5. Город'
;