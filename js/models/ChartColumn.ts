import { ChartColumnPattern } from './ChartColumnPattern';
import { ChartColumnType } from './ChartColumnType';

export interface ChartColumn {
  type: ChartColumnType;
  label: string;
  pattern: ChartColumnPattern | null;
}
