<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <html>
            <head>
                <title>База игроков проекта "Discord onlien"</title>
            </head>
            <body>
                <h1>Игровые уровни</h1>
                <table border="0.5">
                    <tr>
                        <th>ID</th>
                        <th>Имя игрока</th>
                        <th>Баланс валюты</th>
                        <th>Общий рейтинг</th>
                    </tr>
                    <xsl:for-each select="player/user">
                        <tr>
                            <td><xsl:value-of select="id"/></td>
                            <td><xsl:value-of select="nickname"/></td>
                            <td><xsl:value-of select="balance"/></td>
                            <td><xsl:value-of select="rating"/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
