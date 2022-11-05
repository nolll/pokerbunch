export interface ChartOptions{
    pointSize: number;
    vAxis?: { minValue: number; } | undefined;
    hAxis?: { format: string; } | undefined;
    legend?: { position: string; } | undefined;
    colors?: string[] | undefined;
    series?: { 1: { type: 'area' } } | undefined;
}