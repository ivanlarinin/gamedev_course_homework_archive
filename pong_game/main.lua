-- CONSTANTS
-- screen variables
WINDOW_WIDTH = 1280
WINDOW_HEIGHT = 720
VIRTUAL_WIDTH = 432
VIRTUAL_HEIGHT = 243

PADDLE_SPEED = 200

-- includes
Class = require 'class'
push = require 'push'
require 'Ball'
require 'Paddle'

-- loads data
function love.load()
    -- randomness include
    math.randomseed(os.time())
    
    -- pixel filter type
    love.graphics.setDefaultFilter('nearest', 'nearest')

    -- FONTS
    -- retro looking font
    retroFontName = '/font/04B_03__.ttf'
    smallFont = love.graphics.newFont(retroFontName, 8)
    scoreFont = love.graphics.newFont(retroFontName, 32)
    victoryFont = love.graphics.newFont(retroFontName, 24)

    love.window.setTitle('Pong')

    -- SOUNDS
    sounds = {
        ['paddle_hit'] = love.audio.newSource('/sounds/paddle_hit.wav', 'static'),
        ['point_scored'] = love.audio.newSource('/sounds/point_scored.wav', 'static'),
        ['wall_hit'] = love.audio.newSource('/sounds/wall_hit.wav', 'static')
    }

    -- player data
    player1Score = 0
    player2Score = 0
    servingPlayer = math.random(2) == 1 and 1 or 2
    winningPlayer = 0

    -- objects instances
    player1 = Paddle(5, 20, 5, 20)
    player2 = Paddle(VIRTUAL_WIDTH - 10, VIRTUAL_HEIGHT - 30, 5, 20)
    ball = Ball(VIRTUAL_WIDTH / 2 - 2, VIRTUAL_HEIGHT / 2 - 2, 5, 5)

    -- game variables
    gameState = 'start'
    drawFPS = 0
    AI = 0

    -- screen setup using push plugin
    push:setupScreen(VIRTUAL_WIDTH, VIRTUAL_HEIGHT, WINDOW_WIDTH, WINDOW_HEIGHT, {
        fullscreen = false,
        vsync = true,
        resizable = true
    })
end

function love.resize(w, h)
    push:resize(w, h)
end

-- players movement
function love.update(dt)
    if gameState == 'play' then
        -- COLLISION
        -- side collision
        if ball.x <= 0 then
            servingPlayer = 2
            player1Score = player1Score + 1
            ball:reset()
            sounds['point_scored']:play()
            if player1Score >= 5 then
                gameState = 'victory'
                winningPlayer = 2
            else 
                gameState = 'serve'
            end
        end
        if ball.x >= VIRTUAL_WIDTH - 4 then
            servingPlayer = 1
            player2Score = player2Score + 1
            ball:reset()
            sounds['point_scored']:play()
            if player2Score >= 5 then
                gameState = 'victory'
                winningPlayer = 1
            else 
                gameState = 'serve'
            end
        end

        -- player collision
        if ball:collides(player1) then
            -- deflects the ball to the right
            ball.dx = -ball.dx
            if ball.dx < 190 then
                ball.dx = ball.dx * 1.2
            end
            ball.x = player1.x + 5
            sounds['paddle_hit']:play()
            ball.dy = -math.random(-60, 200)
        end
        if ball:collides(player2) then
            -- deflects the ball to the left
            ball.dx = -ball.dx
            if ball.dx < 190 then
                ball.dx = ball.dx * 1.2
            end
            ball.x = player2.x - 5
            sounds['paddle_hit']:play()
            ball.dy = -math.random(-60, 200)
        end

        -- vertical walls collision
        if ball.y <= 0 then
            ball.dy = -ball.dy
            ball.y = 0
            sounds['wall_hit']:play()
        end
        if ball.y >= VIRTUAL_HEIGHT - 4 then
            ball.dy = -ball.dy
            ball.y = VIRTUAL_HEIGHT - 4
            sounds['wall_hit']:play()
        end
    end

    player1:update(dt)
    player2:update(dt)

    -- player 1
    if love.keyboard.isDown('w') then
        player1.dy = -PADDLE_SPEED
    elseif love.keyboard.isDown('s') then
        player1.dy = PADDLE_SPEED
    else
        player1.dy = 0
    end

    -- player 2
    if AI == 0 then
        if love.keyboard.isDown('up') then
            player2.dy = -PADDLE_SPEED
        elseif love.keyboard.isDown('down') then
            player2.dy = PADDLE_SPEED
        else
            player2.dy = 0
        end
    else
        trh = 1;
        ballPos = ball.y
        playerPos = player2.y
        dist = (ballPos - playerPos) / 4
        if (dist >= -trh and dist <= trh) then
            dist = 0
        end
        player2.dy = PADDLE_SPEED * dist
    end

    if gameState == 'play' then
        ball:update(dt)
    end
end

-- keyboard handle
function love.keypressed(key)
    if key == 'escape' then

        -- terminates the application
        love.event.quit()
    elseif key == 'enter' or key == 'return' then
        if gameState == 'start' then
            gameState = 'serve'
        elseif gameState == 'victory' then
            gameState = 'serve'
            player1Score = 0
            player2Score = 0
        elseif gameState == 'serve' then
            gameState = 'play'
        end
    elseif key == 'm' then
        AI = 1
        if gameState == 'start' then
            gameState = 'serve'
        end
    end

    if key == 'f' then
        drawFPS = 1 - drawFPS
    end
end

-- graw functions
function love.draw()
    push:apply('start')

    -- background color
    love.graphics.clear(40/255, 45/255, 52/255, 255/255)

    -- Messages Draw
    if gameState == 'start' then
        love.graphics.setFont(smallFont)
        love.graphics.printf("Welcome to Pong!", 0, 10, VIRTUAL_WIDTH, 'center')
        love.graphics.printf("Play single match - Enter", 0, 20, VIRTUAL_WIDTH, 'center')
        love.graphics.printf("Play with a human player - M", 0, 30, VIRTUAL_WIDTH, 'center')
    elseif gameState == 'serve' then
        love.graphics.setFont(smallFont)
        love.graphics.printf("Player " .. tostring(servingPlayer) .. "'s turn!", 0, 10, VIRTUAL_WIDTH, 'center')
        love.graphics.printf("Press Enter to Serve", 0, 20, VIRTUAL_WIDTH, 'center')
    elseif gameState == 'victory' then
        -- draw victory message
        love.graphics.setFont(victoryFont)
        love.graphics.printf("Player " .. tostring(winningPlayer) .. " wins!", 0, 10, VIRTUAL_WIDTH, 'center')
        love.graphics.setFont(smallFont)
        love.graphics.printf("Press Enter to Serve", 0, 42, VIRTUAL_WIDTH, 'center')
    elseif gameState == 'play' then
        -- draw
    end

    -- render
    ball:render()
    player1:render()
    player2:render()
    
    -- FPS times
    if drawFPS == 1 then
        displayFPS()
    end

    displayScore()

    push:apply('end')
end

function displayFPS()
    love.graphics.setColor(0, 1, 0, 1)
    love.graphics.setFont(smallFont)
    love.graphics.print('FPS: ' .. tostring(love.timer.getFPS()), 40, 20)
    love.graphics.setColor(1, 1, 1, 1)
end

function displayScore()
    -- score draw
    love.graphics.setFont(scoreFont)
    love.graphics.print(player1Score, VIRTUAL_WIDTH / 2 - 50, VIRTUAL_HEIGHT / 3)
    love.graphics.print(player2Score, VIRTUAL_WIDTH / 2 + 30, VIRTUAL_HEIGHT / 3)
end
