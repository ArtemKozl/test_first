Приложение работает с БД PostgreSQl.
Для корректной работы необходимо создать бд и изменить строку подключения в appsettings.json => appsettings.Development.json.
В этой бд необходимо создать следующие таблицы:
1) Таблицы с доставками

CREATE TABLE IF NOT EXISTS "Deliveries" (
    id SERIAL PRIMARY KEY,
    weight INT NOT NULL,
    district VARCHAR(100) NOT NULL,
    delivery_date TIMESTAMP NOT NULL
);

-- Немного тестовых данных --

INSERT INTO "Deliveries" (id, weight, district, delivery_date)
VALUES 
(1, 2, 'Samoilovo', '2024-10-22 20:30:45'),
(2, 1, 'Durovo', '2024-10-22 09:30:45'),
(3, 3, 'Durovo', '2024-10-22 12:30:45'),
(4, 41, 'Durovo', '2024-10-22 14:33:45'),
(5, 4, 'Semenkovo', '2023-10-22 14:30:45'),
(6, 3, 'Semenkovo', '2023-10-22 14:35:45'),
(7, 2, 'Semenkovo', '2024-10-22 14:30:45'),
(8, 2, 'Samoilovo', '2024-10-22 15:33:45'),
(9, 1, 'Popovka', '2024-10-22 14:30:45');

2)Таблица для сохранения результатов сортировки

create table "DeliveryResultReport" (
	id serial primary key,
	report_date timestamp not null,
	result_report jsonb not null
)

Результат сортировки сохряняется в виде jsob.
Логи записываются в папку logs в test_first.API.

Тестировать можно через swagger:

1)
Пример данных для /Delivery/MoreOrLess

{
"numberOfDeliveries": 2,
"conditionMoreOrLess": ">",
"startDate": "2023-10-22 14:30:45",
"endDate": "2024-10-22 20:30:45"
}

2)
Пример данных для /Delivery/FirstDelivery

{
  "districtName": "Semenkovo",
  "startDate": "2023-10-22T14:20:45"
}

P. S.

Сейчас изменился исходник тестового задания, из-за чего может быть немного не совпадающие с тз названия и есть лишняя логика,
но все условия были выполнены.

Еще хотел все это собрать в Докере, чтобы вам не пришлось с таблицами мучаться, но спросить "А можно?" было не у кого, сделал так.