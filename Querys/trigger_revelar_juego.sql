-- Todos los juegos que hay que revelar y si su nivel
select 
	--game_dependation.id_game, game_dependation.id_dependent_game, game.level_code_pass, level_results.*
	*
from 
	game_dependation 
	left outer join game on game_dependation.id_game = game.id -- traerme todo lo que puedo reverlar
	left outer join level_results on level_results.id_user = 1 and level_results.level_code = game.level_code_pass
group by id_game, id_dependent_game 


-- calculo de juegos niveles necesario superados por usuario
select 
	id_dependent_game as id_game,
	sum(win) as niveles_superados,
	count(id_dependent_game)
from 
	(
		select 
			game_dependation.id_game, game_dependation.id_dependent_game, level_results.*
			--*
		from 
			game_dependation 
			left outer join game on game_dependation.id_game = game.id -- traerme todo lo que puedo reverlar
			left outer join level_results on level_results.id_user = 1 and level_results.level_code = game.level_code_pass
		group by id_game, id_dependent_game 
	) test
group by id_dependent_game

-- Juegos que se pueden revelar
select *
from 
	(
		select 
			id_dependent_game as id_game,
			sum(win) as overcome_levels,
			count(id_dependent_game) as total_levels
		from 
			(
				select 
					game_dependation.id_game, game_dependation.id_dependent_game, level_results.*
				from 
					game_dependation 
					left outer join game on game_dependation.id_game = game.id -- traerme todo lo que puedo reverlar
					left outer join level_results on level_results.id_user = 1 and level_results.level_code = game.level_code_pass
				group by id_game, id_dependent_game 
			) dependent_game_with_results
		group by id_dependent_game
	) games_result
where 
	overcome_levels = total_levels

--CREAR TRIGGER
-- 	este trigger va a buscar los juegos que ya hayan superado el level de cada uno de los que depende
--	para activar el juego al usuario

CREATE TRIGGER LR_next_game AFTER INSERT ON level_results
BEGIN
	insert into enabled_game_user  select NEW.id_user as id_user, games_result.id_game from 
	(
		select 
			id_dependent_game as id_game,
			sum(win) as overcome_levels,
			count(id_dependent_game) as total_levels
		from 
			(
				select 
					game_dependation.id_game, game_dependation.id_dependent_game, level_results.*
				from 
					game_dependation 
					left outer join game on game_dependation.id_game = game.id -- traerme todo lo que puedo reverlar
					left outer join level_results on level_results.id_user = NEW.id_user and level_results.level_code = game.level_code_pass
				group by id_game, id_dependent_game 
			) dependent_game_with_results
		group by id_dependent_game
	) games_result
	left outer join enabled_game_user on games_result.id_game = enabled_game_user.id_game and enabled_game_user.id_user = NEW.id_user
	where 
		overcome_levels = total_levels and enabled_game_user.id_user is null
	;
END