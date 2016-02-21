--##############################################################################
--Calculo de juegos que se deben desplegar para el usuario [OLD]

select game.*, category.name as c_n 
from 
	game  
	left outer join level_results on game.level_code_pass = level_results.level_code
	join category on game.id_category = category.id
	where 
	game.is_default = 1 or level_results.level_code is not null;
	
	
--##############################################################################
--Calculo de juegos que se deben desplegar para el usuario [OLD]

1 -> Juegos default
2 -> Juegos que ha descubierto

select game.*
from game_user join game on game_user.id_game = game.id 
where game_user.id_user = 1
	union
select game.*
from game
where game.is_default = 1
;


-- Juegos Activos
-- juegos que sean default y los que se encuentren abilitados para el usuario
select * from game left outer join enabled_game_user on game.id=enabled_game_user.id_game and enabled_game_user.id_user = [IdUser] where game.is_default = 1 or enabled_game_user.id_user not null