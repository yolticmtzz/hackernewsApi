# Hacker News API

## Descripción

Esta API proporciona acceso a los datos más recientes de Hacker News. Permite recuperar historias nuevas y aplicar filtros de búsqueda.

## Funcionalidades

- **Obtener historias más recientes**: Recupera las historias más recientes desde Hacker News.
- **Filtrar historias por término de búsqueda**: Aplica un filtro de búsqueda a las historias recuperadas.
- **Paginación**: Soporta paginación para navegar a través de las historias.

## Endpoints

### Obtener Historias Más Recientes

**GET** `/api/Stories/newest`

Recupera las historias más recientes desde Hacker News.

**Parámetros de Consulta:**

- `searchTerm` (opcional): Término de búsqueda para filtrar las historias por título.
- `page` (requerido): Número de la página de resultados.
- `pageSize` (requerido): Número de historias por página.

**Ejemplo de Solicitud:**

```http
GET /api/Stories/newest?page=1&pageSize=10&searchTerm=angular
