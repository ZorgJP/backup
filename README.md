# backup
backuper for files on hdd

Резервное копирование файлов.
Консольное приложение для резервного копирования файлов в архив.

В файле настроек хранятся пути для исходной и целевой папки.
При запуске программы происходит создание папки с временным штампом в целевой папке и копирование в неё всех доступных файлов из исходной. Требуется обрабатывать ситуации с невозможностью доступа к файлам в исходной папке.

Пункты со звездочкой являются дополнительными и не обязательны для выполнения.

* Файл настроек имеет формат JSON.
* Есть возможность указать несколько исходных папок.
* Ведется журналирование процесса копирования. Уровень журналирования можно указать в файле настроек.
Примеры распределения событий:
Error - Ошибки приложения. Например, те, которые вызвали неожиданное падение.
Info - Основные события приложения: старт приложения, обработка одной исходной папки или обработанные ошибки.
Debug - Отладочная информация. Например, скопирован отдельный файл
