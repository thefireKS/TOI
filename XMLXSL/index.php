<?php
// Загрузка XML и XSL файлов
$xml = new DOMDocument;
$xml->load('player_base.xml');

$xsl = new DOMDocument;
$xsl->load('player_style.xsl');

// Создание объекта XSLTProcessor
$proc = new XSLTProcessor;

// Привязка XSL таблицы стилей
$proc->importStyleSheet($xsl);

// Преобразование и вывод результата
echo $proc->transformToXML($xml);
?>